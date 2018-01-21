﻿using Caliburn.Micro;
using Ninject;
using System;
using System.Windows.Input;
using TextGrunt.Models;
using TextGrunt.Services;

namespace TextGrunt.ViewModels
{
    public class MainViewModel : Conductor<IShellTabItem>.Collection.OneActive
    {
        private StandardKernel _kernel;
        private IDialogService _dialogService;
        private IStorageService _storageService;
        private IEventAggregator _eventAggregator;
        private IBookService _bookService;

        public MainViewModel(StandardKernel kernel, IDialogService dialogService, IEventAggregator eventAggregator, IBookService bookService, IStorageService storageService)
        {
            IsClosed = true;
            _kernel = kernel;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            DisplayName = "TextGrunt";
            _bookService = bookService;
            _storageService = storageService;
        }

        public ICommand NewTabCommand => new RelayCommand(o => true, o => AddNewTab());
        public ICommand AboutCommand => new RelayCommand(o => true, o => _dialogService.ShowAbout());
        public ICommand OptionsCommand => new RelayCommand(o => true, o => _dialogService.ShowOptions());
        public ICommand RemoveActiveCommand => new RelayCommand(o => HasActive(), o => AskAndRemoveActive());
        public ICommand RenameActiveCommand => new RelayCommand(o => HasActive(), o => AskAndRenameActive());
        public ICommand CloseCommand => new RelayCommand(o => true, o => TryClose());
        public ICommand ImportCommand => new RelayCommand(o => true, o => ImportFromFile());
        public ICommand ExportActiveCommand => new RelayCommand(o => HasActive(), o => ExportActive());
        public ICommand OpenHelpCommand => new RelayCommand(o => true, o => System.Diagnostics.Process.Start($"help.txt"));
        public bool IsClosed { get; set; }

        //
        // Summary:
        //     Called when activating.
        protected override void OnActivate()
        {
            foreach (var sheet in _bookService.Book.Sheets)
            {
                Display(sheet);
            }

            Items.Add(_kernel.Get<ClipboardViewModel>());


            IsClosed = false;
            base.OnActivate();
        }

        //
        // Summary:
        //     Called when deactivating.
        //
        // Parameters:
        //   close:
        //     Inidicates whether this instance will be closed.
        protected override void OnDeactivate(bool close)
        {
            if (close)
                IsClosed = true;
            base.OnDeactivate(close);
        }

        private void AddNewTab()
        {
            var sheet = _bookService.BuildNewSheet();
            _bookService.Book.Sheets.Add(sheet);
            Display(sheet);
        }

        private void ImportFromFile()
        {
            string path = _dialogService.GetUserFileOpenInput();
            if (path == null)
                return;
            var sheet = _storageService.Read<Sheet>(path);
            if (sheet == null)
            {
                _dialogService.ShowError($"Unable to import {path}.");
                return;
            }

            _bookService.Book.Sheets.Add(sheet);
            Display(sheet);
        }

        private void Display(Sheet sheet)
        {
            var vm = _kernel.Get<TabViewModel>();
            vm.Sheet = sheet;
            Items.Add(vm);
            ActiveItem = vm;
        }

        private void ExportActive()
        {
            string path = _dialogService.GetUserFileSaveInput();
            if (path == null)
                return;

            var sheet = GetActive().Sheet;

            if (!_storageService.Write(sheet, path))
            {
                _dialogService.ShowError($"Can't save active tab to {path}");
            }
        }

        private bool HasActive()
        {
            TabViewModel vm = GetActive();
            return vm != null;
        }

        private TabViewModel GetActive()
        {
            return ActiveItem as TabViewModel;
        }

        private void AskAndRemoveActive()
        {
            var vm = GetActive();

            if (_dialogService.ShowOkCancel($"Really remove {vm.Sheet.Name} tab?"))
            {
                _bookService.Book.Sheets.Remove(vm.Sheet);
                vm.TryClose();
            }
        }

        private void AskAndRenameActive()
        {
            var vm = GetActive();
            string newName = _dialogService.GetUserTextInput($"Rename {vm.DisplayName}", "Enter new name", vm.DisplayName, false);
            if (newName == null)
                return;
            vm.DisplayName = newName;
        }
    }
}