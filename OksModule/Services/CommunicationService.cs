using OksModule.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OksModule.Services
{
    public class CommunicationService
    {
        private readonly HttpClient _httpClient;
        private const string ReceiverUrl = "http://localhost:8080/receive/document";

        public CommunicationService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> SendDocumentToDepartment(Document document)
        {
            try
            {
                
                var json = JsonSerializer.Serialize(new
                {
                    document.DocumentId,
                    document.DocumentType,
                    document.Title,
                    document.Status,
                    document.CreatedDate,
                    RecipientDepartment = document.RecipientDepartmentId
                });

                // Создаем содержимое запроса
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем запрос
                var response = await _httpClient.PostAsync(ReceiverUrl, content);

                // Возвращаем результат
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // В реальном приложении здесь должно быть логирование ошибки
                Console.WriteLine($"Ошибка при отправке документа: {ex.Message}");
                return false;
            }
        }
    }
}

