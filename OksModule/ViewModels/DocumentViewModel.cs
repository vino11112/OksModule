using Microsoft.Win32;
using OksModule.Commands;
using OksModule.Models;
using OksModule.Services;
using OksModule.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace OksModule.ViewModels
{
    public class DocumentViewModel : ViewModelBase
    {
        private readonly DatabaseService _dbService = new DatabaseService();
        private Document _document;
        public Document Document
        {
            get => _document;
            set
            {
                _document = value;
                OnPropertyChanged();
                UpdateCommandsState();
            }
        }

        public ObservableCollection<string> DocumentTypes { get; } = new ObservableCollection<string>
        {
            "Технические условия",
            "Расчет стоимости",
            "Проектная документация",
            "Запрос на изыскания"
        };

        public ObservableCollection<Department> Departments { get; } = new ObservableCollection<Department>();
        public ObservableCollection<ConstructionProject> Projects { get; } = new ObservableCollection<ConstructionProject>();

        public ICommand SaveCommand { get; }
        public ICommand ApproveCommand { get; }
        public ICommand SendCommand { get; }
        public ICommand BrowseFileCommand { get; }
        public ICommand OpenDocumentsListCommand { get; }

        public DocumentViewModel()
        {
            // Инициализация документа
            Document = new Document();

            // Инициализация команд
            SaveCommand = new RelayCommand(SaveDocument, _ => true);
            ApproveCommand = new RelayCommand(ApproveDocument, CanApproveDocument);
            SendCommand = new RelayCommand(SendDocument, CanSendDocument);
            BrowseFileCommand = new RelayCommand(BrowseFile, _ => true);
            OpenDocumentsListCommand = new RelayCommand(_ => OpenDocumentsList());

            // Загрузка тестовых данных
            LoadTestData();
        }
        private void OpenDocumentsList()
        {
            var documentsWindow = new DocumentsListView();
            documentsWindow.Show();
        }
        private void LoadTestData()
        {
            Departments.Add(new Department { DepartmentId = 1, Name = "ПТО", Code = "PTO" });
            Departments.Add(new Department { DepartmentId = 2, Name = "Юридический отдел", Code = "LEGAL" });

            Projects.Add(new ConstructionProject { ProjectId = 1, Title = "Проект газификации" });
            Projects.Add(new ConstructionProject { ProjectId = 2, Title = "Реконструкция газопровода" });
        }

        private void SaveDocument(object parameter)
        {

            try
            {
                int documentId = _dbService.SaveDocument(Document);
                Document.DocumentId = documentId;

                MessageBox.Show($"Документ сохранен с ID: {documentId}",
                              "Успех",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }
        

        private bool CanApproveDocument(object parameter)
        {
            return Document != null &&
                  (Document.Status == "Черновик" || Document.Status == "На утверждении");
        }

        private void ApproveDocument(object parameter)
        {
            Document.Status = "Утвержден";
            OnPropertyChanged(nameof(Document));
            UpdateCommandsState();
            MessageBox.Show("Документ утвержден!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanSendDocument(object parameter)
        {
            return Document != null &&
                  Document.Status == "Утвержден" &&
                  Document.RecipientDepartmentId.HasValue;
        }

        private  async void SendDocument(object parameter)
        {
            try
            {
                var communicationService = new CommunicationService();
                bool success = await communicationService.SendDocumentToDepartment(Document);

                if (success)
                {
                    Document.Status = "Отправлен";
                    MessageBox.Show("Документ успешно отправлен!", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при отправке документа", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BrowseFile(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Document.FilePath = openFileDialog.FileName;
                OnPropertyChanged(nameof(Document));
            }
        }

        private void UpdateCommandsState()
        {
            (ApproveCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (SendCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
}
