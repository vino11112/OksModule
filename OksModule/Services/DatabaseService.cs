using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using OksModule.Models;
namespace OksModule.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            _connectionString = "Server=DESKTOP-VVN5VCI\\SQLEXPRESS;Database=OksModuleDB;Integrated Security=True;";
        }

        public int SaveDocument(Document document)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"INSERT INTO Documents 
                            (DocumentType, Title, Content, Status, CreatedDate, Deadline, AuthorId, RecipientDepartmentId, FilePath)
                            VALUES 
                            (@DocumentType, @Title, @Content, @Status, @CreatedDate, @Deadline, @AuthorId, @RecipientDepartmentId, @FilePath);
                            SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DocumentType", document.DocumentType);
                    command.Parameters.AddWithValue("@Title", document.Title);
                    command.Parameters.AddWithValue("@Content", document.Content ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", document.Status);
                    command.Parameters.AddWithValue("@CreatedDate", document.CreatedDate);
                    command.Parameters.AddWithValue("@Deadline", document.Deadline ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AuthorId", document.AuthorId);
                    command.Parameters.AddWithValue("@RecipientDepartmentId", document.RecipientDepartmentId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FilePath", document.FilePath ?? (object)DBNull.Value);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
        public void UpdateDocument(Document document)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"UPDATE Documents SET 
                     DocumentType = @DocumentType,
                     Title = @Title,
                     Content = @Content,
                     Status = @Status,
                     Deadline = @Deadline,
                     RecipientDepartmentId = @RecipientDepartmentId,
                     FilePath = @FilePath
                     WHERE DocumentId = @DocumentId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DocumentId", document.DocumentId);
                    command.Parameters.AddWithValue("@DocumentType", document.DocumentType);
                    command.Parameters.AddWithValue("@Title", document.Title);
                    command.Parameters.AddWithValue("@Content", document.Content ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", document.Status);
                    command.Parameters.AddWithValue("@Deadline", document.Deadline ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RecipientDepartmentId", document.RecipientDepartmentId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FilePath", document.FilePath ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Document> GetAllDocuments()
        {
            var documents = new List<Document>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Documents ORDER BY CreatedDate DESC";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        documents.Add(new Document
                        {
                            DocumentId = Convert.ToInt32(reader["DocumentId"]),
                            DocumentType = reader["DocumentType"].ToString(),
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"] != DBNull.Value ? reader["Content"].ToString() : null,
                            Status = reader["Status"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            Deadline = reader["Deadline"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["Deadline"]) : null,
                            AuthorId = Convert.ToInt32(reader["AuthorId"]),
                            RecipientDepartmentId = reader["RecipientDepartmentId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["RecipientDepartmentId"]) : null,
                            FilePath = reader["FilePath"] != DBNull.Value ? reader["FilePath"].ToString() : null
                            
                        });
                    }
                }
            }

            return documents;
        }
    }
}
