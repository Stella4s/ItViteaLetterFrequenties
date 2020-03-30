using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using ItViteaLetterFrequenties.Model;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace ItViteaLetterFrequenties.Viewmodel
{
    public class VMLetterInfo : INotifyPropertyChanged
    {
        #region privateproperties
        private string _TextInput, _InfoBox;
        private LetterInfoList _LetterList;
        private bool _IsChecked1, _IsChecked2;
        private bool LetterListFilter(object item)
        {
            LetterInfo letterI = item as LetterInfo;
            return letterI.IsLetter == true;
        }
        #endregion
        public VMLetterInfo()
        {
            LetterList = new LetterInfoList();
            LLCollectionView = CollectionViewSource.GetDefaultView(LetterList);
            SetFrequencyList();
            if (LLCollectionView  != null && LLCollectionView.CanSort == true)
            {
                LLCollectionView.SortDescriptions.Clear();
                LLCollectionView.SortDescriptions.Add(new SortDescription("Letter", ListSortDirection.Ascending));
            }
        }
        #region Public Properties
        public ICollectionView LLCollectionView { get; }
        public LetterInfoList LetterList
        {
            get { return _LetterList; }
            set
            {
                _LetterList = value;
                OnPropertyChanged("LetterList");
            }
        }
        public string TextInput
        {
            get { return _TextInput; }
            set
            {
                _TextInput = value;
                OnPropertyChanged("TextInput");
            }
        }
        public string InfoBox
        {
            get { return _InfoBox; }
            set
            {
                _InfoBox = value;
                OnPropertyChanged("InfoBox");
            }
        }
        public bool IsChecked1
        {
            get { return _IsChecked1; }
            set
            {
                _IsChecked1 = value;
                OnPropertyChanged("IsChecked1");
            }
        }
        public bool IsChecked2
        {
            get { return _IsChecked2; }
            set
            {
                _IsChecked2 = value;
                OnPropertyChanged("IsChecked2");
            }
        }
        #endregion

        #region Commands
        public ICommand GetListCommand
        {
            get
            {
                return new RelayCommand(SetList);
            }
        }
        public ICommand OpenFileCommand
        {
            get
            {
                return new RelayCommand(OpenFile);
            }
        }
        public ICommand LoadFileCommand
        {
            get
            {
                return new RelayCommand(LoadFile);
            }
        }
        public ICommand ToggleCharactersCommand
        {
            get
            {
                return new RelayCommand(ToggleCharacters);
            }
        } 
        #endregion

        #region Methods
        public void SetList()
        {
            MakeList(TextInput);
        }
        public void MakeList(string str)
        {
            if (str != null)
            {
                LetterList.FillLetterList(str);
                SetFrequencyList();
            }
            else
                TextInput = "Error.";
        }
        public void SetFrequencyList()
        {
            double dblSum = LetterList.Sum(ltrList => ltrList.Count);

            foreach (var item in LetterList)
            {
                item.Frequency = (item.Count / dblSum);
            }
        }
        public void SetFrequentcyFilterList()
        {
            var filteredList = from LetterInfo item in LetterList
                               where item.IsLetter == true
                               select item;
            double dblSum = filteredList.Sum(ltrList => ltrList.Count);

            foreach (var item in filteredList)
            {
                item.Frequency = (item.Count / dblSum);
            }
        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                TextInput = File.ReadAllText(openFileDialog.FileName);
                InfoBox = openFileDialog.SafeFileName.ToString();
            }
        }
        private void LoadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                MakeList(File.ReadAllText(openFileDialog.FileName));
                InfoBox = openFileDialog.SafeFileName.ToString();
            }
        }
        private void ToggleCharacters()
        {
            if(IsChecked1)
            {
                LLCollectionView.Filter = LetterListFilter;
                SetFrequentcyFilterList();
            }
            else
            {
                LLCollectionView.Filter = null;
                SetFrequencyList();
            }
            LLCollectionView.Refresh();
        }
        #endregion

        #region INotifyPropertyChanged Members  
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
