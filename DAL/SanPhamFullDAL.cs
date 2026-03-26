using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;

namespace DAL
{
    public class SanPhamFullDAL : ISanPhamFullDAL
    {
        private readonly DatabaseContext _context;

        public SanPhamFullDAL(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateFull(
            string maSanPham,
            int idDanhMuc,
            string tenSanPham,
            string moTa,
            string bienTheJson,
            List<(string fileName, bool laAnhChinh, int thuTu, int indexBienThe)> images
        )
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var tran = await _context.Database.BeginTransactionAsync();
                try
                {
                    // ===== 1. SẢN PHẨM =====
                    var sanPham = new SanPham
                    {
                        MaSanPham = maSanPham,
                        IdDanhMuc = idDanhMuc,
                        IdTrangThai = 1
                    };

                    _context.SanPhams.Add(sanPham);
                    await _context.SaveChangesAsync();

                    int idSanPham = sanPham.IdSanPham;

                    // ===== 2. NGÔN NGỮ =====
                    var languages = _context.NgonNgus.ToList();

                    foreach (var lang in languages)
                    {
                        string ten = tenSanPham;
                        string mota = moTa;

                        if (lang.MaNgonNgu != "vi")
                        {
                            ten = RemoveVietnameseSign(tenSanPham);
                            mota = RemoveVietnameseSign(moTa);
                        }

                        _context.SanPhamLangs.Add(new SanPhamLang
                        {
                            IdSanPham = idSanPham,
                            MaNgonNgu = lang.MaNgonNgu,
                            TenSanPham = ten,
                            MoTa = mota
                        });
                    }

                    await _context.SaveChangesAsync();

                    // ===== 3. PARSE BIẾN THỂ =====
                    var bienThes = JsonConvert.DeserializeObject<List<BienTheSanPhamDTO>>(bienTheJson);

                    for (int i = 0; i < bienThes.Count; i++)
                    {
                        var bt = bienThes[i];

                        // ===== 4. BIẾN THỂ =====
                        var bienThe = new BienTheSanPham
                        {
                            MaBienThe = bt.MaBienThe,
                            IdSanPham = idSanPham,
                            GiaBan = bt.GiaBan ?? 0,
                            TonKho = bt.TonKho,
                            MaSKU = bt.MaSKU,
                            IdTrangThai = bt.IdTrangThai
                        };

                        _context.BienTheSanPhams.Add(bienThe);
                        await _context.SaveChangesAsync();

                        int idBienThe = bienThe.IdBienThe;

                        // ===== 5. ẢNH =====
                        var imgs = images.Where(x => x.indexBienThe == i).ToList();

                        foreach (var img in imgs)
                        {
                            _context.HinhAnhSanPhams.Add(new HinhAnhSanPham
                            {
                                MaHinhAnh = Guid.NewGuid().ToString(),
                                IdBienThe = idBienThe,
                                DuongDanAnh = img.fileName,
                                LaAnhChinh = img.laAnhChinh,
                                ThuTu = img.thuTu
                            });
                        }

                        // ===== 6. THUỘC TÍNH BIẾN THỂ ⭐ =====
                        if (bt.ThuocTinhs != null)
                        {
                            foreach (var tt in bt.ThuocTinhs)
                            {
                                _context.BienTheThuocTinhs.Add(new BienTheThuocTinh
                                {
                                    IdBienThe = idBienThe,
                                    IdGiaTri = tt.IdGiaTri
                                });
                            }
                        }
                    }

                    await _context.SaveChangesAsync();

                    await tran.CommitAsync();
                    return true;
                }
                catch
                {
                    await tran.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<bool> UpdateFull(
    int idSanPham,
    string maSanPham,
    int idDanhMuc,
    string tenSanPham,
    string moTa,
    string bienTheJson,
    List<(string fileName, bool laAnhChinh, int thuTu, int indexBienThe)> images,
    List<string> oldImagesToDelete
)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var tran = await _context.Database.BeginTransactionAsync();
                try
                {
                    // ===== 1. UPDATE SẢN PHẨM =====
                    var sp = _context.SanPhams.FirstOrDefault(x => x.IdSanPham == idSanPham);
                    if (sp == null) return false;

                    sp.MaSanPham = maSanPham;
                    sp.IdDanhMuc = idDanhMuc;

                    // ===== 2. UPDATE NGÔN NGỮ =====
                    var langs = _context.SanPhamLangs.Where(x => x.IdSanPham == idSanPham).ToList();

                    foreach (var lang in langs)
                    {
                        if (lang.MaNgonNgu == "vi")
                        {
                            lang.TenSanPham = tenSanPham;
                            lang.MoTa = moTa;
                        }
                        else
                        {
                            lang.TenSanPham = RemoveVietnameseSign(tenSanPham);
                            lang.MoTa = RemoveVietnameseSign(moTa);
                        }
                    }

                    // ===== 3. PARSE BIẾN THỂ =====
                    var bienThes = JsonConvert.DeserializeObject<List<BienTheSanPhamDTO>>(bienTheJson);

                    var bienTheIds = new List<int>();

                    for (int i = 0; i < bienThes.Count; i++)
                    {
                        var bt = bienThes[i];

                        BienTheSanPham entity;

                        // ===== 4. INSERT / UPDATE BIẾN THỂ =====
                        if (bt.IdBienThe > 0)
                        {
                            entity = _context.BienTheSanPhams.First(x => x.IdBienThe == bt.IdBienThe);

                            entity.MaBienThe = bt.MaBienThe;
                            entity.GiaBan = bt.GiaBan ?? 0;
                            entity.TonKho = bt.TonKho;
                            entity.MaSKU = bt.MaSKU;
                            entity.IdTrangThai = bt.IdTrangThai;
                        }
                        else
                        {
                            entity = new BienTheSanPham
                            {
                                MaBienThe = bt.MaBienThe,
                                IdSanPham = idSanPham,
                                GiaBan = bt.GiaBan ?? 0,
                                TonKho = bt.TonKho,
                                MaSKU = bt.MaSKU,
                                IdTrangThai = bt.IdTrangThai
                            };

                            _context.BienTheSanPhams.Add(entity);
                            await _context.SaveChangesAsync();
                        }

                        bienTheIds.Add(entity.IdBienThe);

                        // ===== 5. XÓA THUỘC TÍNH CŨ =====
                        var oldTT = _context.BienTheThuocTinhs
                            .Where(x => x.IdBienThe == entity.IdBienThe);

                        _context.BienTheThuocTinhs.RemoveRange(oldTT);

                        // ===== 6. ADD THUỘC TÍNH MỚI =====
                        if (bt.ThuocTinhs != null)
                        {
                            foreach (var tt in bt.ThuocTinhs)
                            {
                                _context.BienTheThuocTinhs.Add(new BienTheThuocTinh
                                {
                                    IdBienThe = entity.IdBienThe,
                                    IdGiaTri = tt.IdGiaTri
                                });
                            }
                        }

                        // ===== 7. ADD ẢNH MỚI =====
                        var imgs = images.Where(x => x.indexBienThe == i).ToList();

                        foreach (var img in imgs)
                        {
                            _context.HinhAnhSanPhams.Add(new HinhAnhSanPham
                            {
                                MaHinhAnh = Guid.NewGuid().ToString(),
                                IdBienThe = entity.IdBienThe,
                                DuongDanAnh = img.fileName,
                                LaAnhChinh = img.laAnhChinh,
                                ThuTu = img.thuTu
                            });
                        }
                    }

                    // ===== 8. XÓA BIẾN THỂ KHÔNG CÒN =====
                    var oldBienThes = _context.BienTheSanPhams
                        .Where(x => x.IdSanPham == idSanPham)
                        .ToList();

                    var deleteBienThes = oldBienThes
                        .Where(x => !bienTheIds.Contains(x.IdBienThe))
                        .ToList();

                    foreach (var bt in deleteBienThes)
                    {
                        // xóa ảnh DB
                        var imgs = _context.HinhAnhSanPhams
                            .Where(x => x.IdBienThe == bt.IdBienThe);

                        _context.HinhAnhSanPhams.RemoveRange(imgs);

                        // xóa thuộc tính
                        var tts = _context.BienTheThuocTinhs
                            .Where(x => x.IdBienThe == bt.IdBienThe);

                        _context.BienTheThuocTinhs.RemoveRange(tts);

                        _context.BienTheSanPhams.Remove(bt);
                    }

                    await _context.SaveChangesAsync();
                    await tran.CommitAsync();

                    return true;
                }
                catch
                {
                    await tran.RollbackAsync();
                    throw;
                }
            });
        }

        // ===== HELPER =====
        private string RemoveVietnameseSign(string text)
        {
            text = text.ToLower();
            string normalized = text.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new System.Text.StringBuilder();

            foreach (char c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c)
                    != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            string result = sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
            result = System.Text.RegularExpressions.Regex.Replace(result, @"[^a-z0-9\s-]", "");
            result = result.Replace(" ", "-");

            return result;
        }
    }
}