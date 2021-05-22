using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using StudentManager.Models;

namespace StudentManager.ViewModels
{
    class MsgBoxAddItemsViewModel
    {
        public MsgBoxAddItemsViewModel()
        {
            ItemsControlSource = new ObservableCollection<AddItemModel> { new AddItemModel { AddedItem = "" } };
        }
        public ObservableCollection<AddItemModel> ItemsControlSource { get; set; }

        public List<string> GetApplyData()
        {
            var ls = new List<string> { };
            foreach (var item in ItemsControlSource)
            {
                ls.Add(item.AddedItem);
            }
            return ls;
        }
    }
}
