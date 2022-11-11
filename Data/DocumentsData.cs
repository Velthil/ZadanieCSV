using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieCSV.Models;

namespace ZadanieCSV.Data
{
    public static class DocumentsData
    {
        public static List<document> ReadDocumentFromCsv()
        {
            List<document> doc = null;
            return doc;
        }
        public static void AddDocumentToDb(document doc)
        {
            using(var db = new dataContext())
            {
                db.Add(doc);
                db.SaveChanges();
            }
        }

        public static void AddItemToDb(item item)
        {
            using (var db = new dataContext())
            {
                db.Add(item);
                db.SaveChanges();
            }
        }

        public static List<document> GetDocuments()
        {
            using (var db = new dataContext())
            {
                return db.documents.OrderBy(d => d.Id).ToList();
            }
        }
    }
}
