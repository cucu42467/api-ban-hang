using System.Net.Http;
using System.Text.Json;

namespace Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _http;

        public ExchangeRateService(HttpClient http)
        {
            _http = http;
        }

        // Map mã ngôn ngữ → mã tiền tệ
        private static readonly Dictionary<string, string> _mapTienTe = new()
        {
            { "en", "USD" },
            { "zh", "CNY" },
            { "ja", "JPY" },
            { "ko", "KRW" },
            { "fr", "EUR" },
            { "de", "EUR" },
            { "es", "EUR" },
            { "ru", "RUB" },
            { "th", "THB" },
            { "id", "IDR" },
            { "ms", "MYR" },
            { "hi", "INR" },
            { "ar", "SAR" }
        };

        public async Task<Dictionary<string, decimal>> LayTiGia()
        {
            var ketQua = new Dictionary<string, decimal>();

            try
            {
                var json = await _http.GetStringAsync(
                    "https://api.exchangerate-api.com/v4/latest/VND");

                var doc = JsonDocument.Parse(json);
                var rates = doc.RootElement.GetProperty("rates");

                foreach (var (maNgonNgu, maTienTe) in _mapTienTe)
                {
                    if (rates.TryGetProperty(maTienTe, out var val))
                    {
                        ketQua[maNgonNgu] = val.GetDecimal();
                    }
                }
            }
            catch (Exception ex)
            {
                // log lỗi nếu có, trả về rỗng để BackgroundService bỏ qua lần này
                Console.WriteLine($"[ExchangeRateService] Lỗi: {ex.Message}");
            }

            return ketQua;
        }
    }
}