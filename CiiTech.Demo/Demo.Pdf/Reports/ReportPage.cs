using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Pdf.Reports
{
    public enum ArrayChangedType
    {
        ItemAdded,
        ItemRemoved,
        ItemMoveAuto,
    }


    public class ArrayChangedEventArgs<ItemType> : EventArgs
    {
        public ItemType Item
        {
            get;
            private set;
        }

        public ArrayChangedType ChangeType
        {
            get;
            private set;
        }

        //We need to set this flag as true to repaint dot plot when a gate is added via redo or undo.
        //Fix bug#5868.
        public bool RefreshWhenAdded
        {
            get;
            private set;
        }

        public ArrayChangedEventArgs(ItemType item, ArrayChangedType type, bool refreshWhenAdded = false)
        {
            Item = item;
            ChangeType = type;
            RefreshWhenAdded = type == ArrayChangedType.ItemAdded && refreshWhenAdded;
        }
    }

    [Serializable]
    public class ReportPage
    {
        // store the UI bounds when shown on plot form, no need to serialize it
        [NonSerialized]
        private Rectangle _bounds;

        /// <summary>
        /// gets or sets the UI bounds when shown on plot form
        /// </summary>
        public Rectangle Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }

        private List<ReportItemBase> _reportItems;

        public ReadOnlyCollection<ReportItemBase> ReportItems
        {
            get { return _reportItems.AsReadOnly(); }
        }

        public ReportPage()
        {
            _reportItems = new List<ReportItemBase>();
        }

        /// <summary>
        /// event fired when report item added or removed
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ArrayChangedEventArgs<ReportItemBase>> ReportItemArrayChanged;

        /// <summary>
        /// fire the ReportItemArrayChanged event
        /// </summary>
        /// <param name="item"></param>
        /// <param name="type"></param>
        private void OnReportItemArrayChanged(ReportItemBase item, ArrayChangedType type)
        {
            if (ReportItemArrayChanged != null)
            {
                ReportItemArrayChanged(this, new ArrayChangedEventArgs<ReportItemBase>(item, type));
            }
        }

        /// <summary>
        /// remove report item from the page and fired the ReportItemArrayChanged event
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool RemoveItem(ReportItemBase item, bool isMove = false)
        {
            bool result = _reportItems.Remove(item);
            if (result) OnReportItemArrayChanged(item, isMove ? ArrayChangedType.ItemMoveAuto : ArrayChangedType.ItemRemoved);

            return result;
        }

        /// <summary>
        /// remove all selected items from the page
        /// </summary>
        public void RemoveSelectedItems()
        {
            var items = _reportItems;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].Selected) RemoveItem(items[i]);
            }
        }

        /// <summary>
        /// add report item to the page and fired the ReportItemArrayChanged event
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ReportItemBase item, bool isMove = false)
        {
            _reportItems.Add(item);
            OnReportItemArrayChanged(item, isMove ? ArrayChangedType.ItemMoveAuto : ArrayChangedType.ItemAdded);
        }

        /// <summary>
        /// sort report items
        /// </summary>
        public void SortItems()
        {
            _reportItems.Sort();
        }

    }
}
