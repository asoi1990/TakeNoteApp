using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace assi1
{
    public class Note
    {
        private int idN;
        private string titleN;
        private string contentN;
        private DateTime dateN;

        public Note() { }

        public Note(int id, string title, string content, DateTime date)
        {
            idN = id;
            titleN = title;
            contentN = content;
            dateN = date;
        }

        public Note(int id, string title, string content)
        {
            idN = id;
            titleN = title;
            contentN = content;
        }

        public void setidN(int id)
        { idN = id; }
        public int getidN()
        { return idN; }

        public void settitleN(string title)
        { titleN = title; }
        public string gettitleN()
        { return titleN; }

        public void setcontentN(string content)
        { contentN = content; }
        public string getcontentN()
        { return contentN; }

        public void setdateN(DateTime date)
        { dateN = date; }
        public DateTime getdateN()
        { return dateN; }

    }
}