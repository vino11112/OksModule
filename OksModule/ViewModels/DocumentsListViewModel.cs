using OksModule.Commands;
using OksModule.Models;
using OksModule.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows;
using System.Windows.Input;

namespace OksModule.ViewModels
{
    public class DocumentsListViewModel : ViewModelBase
    {
        private readonly DatabaseService _dbService = new DatabaseService();
        private readonly CommunicationService _commService = new CommunicationService();

        private Document _selectedDocument;
        public Document SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                if (_selectedDocument != value)
                {
                    _selectedDocument = value;
                    OnPropertyChanged();
                    RefreshCommandsState();
                }
            }
        }
        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public List<Document> Documents { get; private set; }

        public ICommand RefreshCommand { get; }
        public ICommand ApproveCommand { get; }
        public ICommand SendCommand { get; }

        public DocumentsListViewModel()
        {
            // Инициализация команд с правильными сигнатурами
            RefreshCommand = new RelayCommand(_ => LoadDocuments());
            ApproveCommand = new RelayCommand(_ => ApproveSelectedDocument(), _ => CanApproveDocument());
            SendCommand = new RelayCommand(_ => SendSelectedDocument(), _ => CanSendDocument());

            LoadDocuments();
        }
        private void RefreshCommandsState()
        {
            (ApproveCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (SendCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
        private bool CanApproveDocument()
        {
            return SelectedDocument != null &&
                  (SelectedDocument.Status == "Черновик" || SelectedDocument.Status == "На утверждении");
        }
        private void ApproveSelectedDocument()
        {
            try
            {
                SelectedDocument.Status = "Утвержден";
                _dbService.UpdateDocument(SelectedDocument);
                MessageBox.Show($"Документ ID {SelectedDocument.DocumentId} утвержден",
                              "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDocuments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при утверждении: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CanSendDocument()
        {
            return SelectedDocument != null &&
                  SelectedDocument.Status == "Утвержден" &&
                  SelectedDocument.RecipientDepartmentId.HasValue;
        }
        private async void SendSelectedDocument()
        {
            try
            {
                bool success = await _commService.SendDocumentToDepartment(SelectedDocument);

                if (success)
                {
                    SelectedDocument.Status = "Отправлен";
                    _dbService.UpdateDocument(SelectedDocument);
                    MessageBox.Show($"Документ ID {SelectedDocument.DocumentId} отправлен",
                                  "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDocuments();
                }
                else
                {
                    MessageBox.Show("Ошибка при отправке документа",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDocuments()
        {
            Documents = _dbService.GetAllDocuments();
            OnPropertyChanged(nameof(Documents));
            StatusMessage = $"Загружено документов: {Documents.Count}";
        }
  
    }
}
