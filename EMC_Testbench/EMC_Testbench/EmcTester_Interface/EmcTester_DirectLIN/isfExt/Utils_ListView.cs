/*
 *   Utils_ListView
 *   begin: 2014-01-20
 *          2015-02-11
 *   last:  2016-01-12
 * 
 */

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

namespace isf_candidates
{
    static public class Utils_ListView
    {

        public delegate bool Delegate_GetTextByIndex(object aSource, UInt16 aIndex, out string aText);
        public delegate bool Delegate_GetTextByIndex2(object aSource, UInt16 aIndex1, UInt16 aIndex2, out string aText);

        /*
         * Set ListView members in Designer (example):
         * 
            xResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            xResult.BackColor = SystemColors.Info;
            xResult.ForeColor = SystemColors.InfoText;
            xResult.FullRowSelect = true;
            xResult.GridLines = true;
            xResult.HideSelection = false;
            xResult.Name = "listView_Clients";
            xResult.UseCompatibleStateImageBehavior = false;
            xResult.View = View.Details;
         */

        static public ListView CreateFromText(object aSource, Delegate_GetTextByIndex aGetColumnName, Delegate_GetTextByIndex2 aGetCellInfo, out string aError)
        {
            ListView xResult = null;
            aError = null;
            try
            {
                xResult = new ListView();
                xResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                xResult.BackColor = SystemColors.Info;
                xResult.ForeColor = SystemColors.InfoText;
                xResult.FullRowSelect = true;
                xResult.GridLines = true;
                xResult.HideSelection = false;
                xResult.Name = "listView_Clients";
                xResult.UseCompatibleStateImageBehavior = false;
                xResult.View = View.Details;

                //
                // populate columns
                //
                if (aGetColumnName != null)
                {
                    UInt16 iColl = 0;
                    while (aGetColumnName(aSource, iColl, out string xColumn))
                    {
                        xResult.Columns.Add(xColumn);
                        iColl++;
                    }

                    //
                    // populate cells
                    //
                    if (aGetCellInfo != null)
                    {
                        UInt16 iRow = 0;
                        while (aGetCellInfo(aSource, iRow, 0, out string xCellInfo))
                        {
                            ListViewItem xItem = new ListViewItem(xCellInfo);
                            xResult.Items.Add(xItem);
                            for (iColl = 1; iColl < xResult.Columns.Count; iColl++)
                            {
                                aGetCellInfo(aSource, iRow, iColl, out xCellInfo);
                                xItem.SubItems.Add(xCellInfo);
                            }
                            iRow++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                aError = ex.Message.ToString();
                xResult = null;
            }
            return xResult;
        }

        static public TClass GetItemClass<TClass>(ListViewItem aItem)
        {
            object xResult = null;
            if (aItem != null)
            {
                xResult = aItem.Tag;
                if (!(xResult is TClass))
                    xResult = null;
            }

            return ((TClass)xResult);
        }

        static public void ToSubItem(ListViewItem aItem, int aSubItem, string aText, object aTag)
        {
            if ((!(aItem == null)) && (aSubItem >= 0))
            {
                while (aItem.SubItems.Count <= aSubItem)
                {
                    aItem.SubItems.Add("");
                }
                aItem.SubItems[aSubItem].Text = aText;
                aItem.SubItems[aSubItem].Tag = aTag;
            }
        }

        static public void ToSubItem(ListViewItem aItem, int aSubItem, string aText)
        {
            if ((!(aItem == null)) && (aSubItem >= 0))
            {
                while (aItem.SubItems.Count <= aSubItem)
                {
                    aItem.SubItems.Add("");
                }
                aItem.SubItems[aSubItem].Text = aText;
            }
        }

        static public void ToSubItem(ListViewItem aItem, int aSubItem, object aTag)
        {
            if ((!(aItem == null)) && (aSubItem >= 0))
            {
                while (aItem.SubItems.Count <= aSubItem)
                {
                    aItem.SubItems.Add("");
                }
                aItem.SubItems[aSubItem].Tag = aTag;
            }
        }

        static public UInt32 SubItemTagInc(ListViewItem aItem, int aSubItem)
        {
            object xTag = GetSubItemTag(aItem, aSubItem);

            UInt32 xResult = xTag is UInt32 ? ((UInt32)xTag) + 1 : 1;

            ToSubItem(aItem, aSubItem, xResult.ToString(), xResult);
            return xResult;

        }

        static public float SubItemTag_dT(ListViewItem aItem, int aSubItem)
        {
            object xTag = GetSubItemTag(aItem, aSubItem);
            DateTime xDateTime_Now = DateTime.Now;

            float xResult = xTag is DateTime ? (float)(xDateTime_Now - (DateTime)xTag).TotalSeconds : float.MaxValue;
            string xResultStr = xResult < 60 ? string.Format("{0:0.000}", xResult) : null;

            ToSubItem(aItem, aSubItem, xResultStr, xDateTime_Now);
            return xResult;
        }

        static public ListViewItem.ListViewSubItem GetSubItem(ListViewItem aItem, int aSubItem)
        {
            ListViewItem.ListViewSubItem xResult = null;
            if ((!(aItem == null)) && (aSubItem >= 0) && (aSubItem < aItem.SubItems.Count))
            {
                xResult = aItem.SubItems[aSubItem];
            }
            return xResult;
        }

        static public object GetSubItemTag(ListViewItem aItem, int aSubItem)
        {
            object xResult = null;
            if (aItem != null)
            {
                if (aSubItem >= 0)
                {
                    ListViewItem.ListViewSubItem subItem = GetSubItem(aItem, aSubItem);
                    if (subItem != null)
                        xResult = subItem.Tag;
                }
                else
                    xResult = aItem.Tag;
            }
            return xResult;
        }

        static public TClass GetSubItemClass<TClass>(ListViewItem aItem, int aSubItem)
        {
            object xResult = GetSubItemTag(aItem, aSubItem);
            if (!(xResult is TClass))
                xResult = null;
            return ((TClass)xResult);
        }

        static public string GetSubItemText(ListViewItem aItem, int aSubItem)
        {
            string xResult = "";
            if (aItem != null)
            {
                if (aSubItem >= 0)
                {
                    ListViewItem.ListViewSubItem subItem = GetSubItem(aItem, aSubItem);
                    if (subItem != null)
                        xResult = subItem.Text;
                }
                else
                    xResult = aItem.Text;
            }
            return xResult;
        }

        static public ListViewItem GetItemFromTag(ListView aListView, int aSubItem, object aTag)
        {
            ListViewItem xResult = null;
            if ((aTag != null) && (aListView != null))
            {
                foreach (ListViewItem item in aListView.Items)
                {
                    object xObj = GetSubItemTag(item, aSubItem);
                    if (aTag.Equals(xObj))
                    {
                        xResult = item;
                        break;
                    }
                }
            }
            return xResult;
        }

        static public ListViewItem GetItemFromTag(ListView aListView, object aTag)
        {
            ListViewItem xResult = null;
            if ((aTag != null) && (aListView != null))
            {
                foreach (ListViewItem item in aListView.Items)
                {
                    if (aTag.Equals(item.Tag))
                    {
                        xResult = item;
                        break;
                    }
                }
            }
            return xResult;
        }

        static public ListViewItem GetItemFromText(ListView aListView, int aSubItem, string aText)
        {
            ListViewItem xResult = null;
            if ((!String.IsNullOrEmpty(aText)) && (aListView != null))
            {
                foreach (ListViewItem item in aListView.Items)
                {
                    if (aText == GetSubItemText(item, aSubItem))
                    {
                        xResult = item;
                        break;
                    }
                }
            }
            return xResult;
        }

        static public ListViewItem GetSelectedItem(ListView aListView)
        {
            if (aListView.SelectedItems.Count > 0)
                return aListView.SelectedItems[0];
            else
                return null;
        }

        static public void CopySelectedItems(ListView aSource, ListView aTarget)
        {
            UnSelectedUnFocused(aTarget);
            foreach (ListViewItem item in aSource.SelectedItems)
            {
                ListViewItem xItem = (ListViewItem)item.Clone();
                aTarget.Items.Add(xItem);
                xItem.Selected = true;
            }
        }

        static public void MoveSelectedItems(ListView aSource, ListView aTarget)
        {
            UnSelectedUnFocused(aTarget);
            while (aSource.SelectedItems.Count > 0)
            {
                ListViewItem xItem = aSource.SelectedItems[0];
                aSource.Items.Remove(xItem);
                aTarget.Items.Add(xItem);
                xItem.Selected = true;
            }
        }

        static public ListViewItem SelectedItem(ListView aListView, int aIndex)
        {
            ListViewItem xResult = null;
            if ((aIndex >= 0) && (aIndex < aListView.Items.Count))
            {
                xResult = aListView.Items[aIndex];
                xResult.Selected = true;
            }
            return xResult;
        }

        static public void UnSelected(ListView aListView)
        {
            aListView.SelectedItems.Clear();
        }

        static public void UnSelectedUnFocused(ListView aListView)
        {
            UnSelected(aListView);
            if (aListView.FocusedItem != null)
                aListView.FocusedItem.Focused = false;
        }

        static public List<string> ToText(ListView aListView)
        {
            List<string> xResult = new List<string>();
            foreach (ListViewItem xItem in aListView.Items)
                xResult.Add(ItemToText(xItem));
            return xResult;
        }

        static public void FromText(ListView aListView, IList aIList)
        {
            aListView.Items.Clear();
            FromTextAppend(aListView, aIList);
        }

        static public void FromTextAppend(ListView aListView, IList aIList)
        {
            foreach (string xLine in aIList)
                FromTextAppend_Item(aListView, xLine);
        }

        static public string ItemToText(ListViewItem aItem)
        {
            string xResult = "";
            foreach (ListViewItem.ListViewSubItem xSubItem in aItem.SubItems)
            {
                if (!String.IsNullOrEmpty(xResult))
                    xResult = xResult + "\t";
                xResult = xResult + xSubItem.Text;
            }
            return xResult;
        }

        static public void FromTextAppend_Item(ListView aListView, string aLine)
        {
            string[] xSubItems = aLine.Split('\t');
            if (xSubItems.Length > 0)
            {
                ListViewItem xItem = aListView.Items.Add(xSubItems[0]);
                for (int i = 1; i < xSubItems.Length; i++)
                    xItem.SubItems.Add(xSubItems[i]);
            }
        }

// TODO the body of this function was in a comment, to determine the current and actualize !!!
        static public void ColumnWidthChanging(ListView aListView, ColumnWidthChangingEventArgs e)
        {
            if (aListView != null)
            {
                int xCollumnWidth = aListView.Columns[e.ColumnIndex].Width - 8;
                foreach (ListViewItem xListViewItem in aListView.Items)
                {
                    Object xObj = GetSubItemTag(xListViewItem, (byte)(e.ColumnIndex));
                    if (xObj is string)
                    {
                        ToSubItem(xListViewItem, (byte)(e.ColumnIndex), Utils_Str.GetCompactedString((string)xObj, aListView.Font, xCollumnWidth));
                    }
                }
            }
        }

        // TODO wip
        static public Rectangle GetSubItemBounds(ListView aListView, ListViewItem aItem, int aSubItem)
        {
            Rectangle subItemRect = Rectangle.Empty;

            if (aItem != null)
            {
                if (aItem.ListView != null)
                {
                    foreach (Control c in aItem.ListView.Controls)
                    {
                        if (c is VScrollBar)
                        {
                            if (c.Visible)
                            {

                            }
                        }
                    }

                    if (aSubItem < aItem.ListView.Columns.Count) //  out of range
                    {
                        // Retrieve the bounds of the entire ListViewItem (all subitems)
                        subItemRect = aItem.GetBounds(ItemBoundsPortion.Entire);
                        int subItemX = subItemRect.Left;

                        // Calculate the X position of the SubItem.
                        // Because the columns can be reordered we have to use Columns[order[i]] instead of Columns[i] !
                        int i;
                        for (i = 0; i < aItem.ListView.Columns.Count; i++)
                        {
                            if (i == aSubItem)
                                break;
                            subItemX += aItem.ListView.Columns[i].Width;
                        }

                        subItemRect = new Rectangle(subItemX, subItemRect.Top, aItem.ListView.Columns[i].Width, subItemRect.Height);
                    }
                }
            }

            return subItemRect;
        }
    } // class Utils_ListView

} // namespace MyUtils_ListView
