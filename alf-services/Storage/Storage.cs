using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.IO;
using System.Configuration;

namespace alf_services.Storage
{
    public class Storage
    {
        public static MongoDatabase database;
        private static MongoServer server;

        public static MemoryStream Get(string filename)
        {
            Connect();
            
            var file = database.GridFS.FindOne(Query.EQ("filename", filename));
            var stream = file.OpenRead();

            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int) stream.Length);

            return new MemoryStream(buffer);
        }

        public static void Store(string filename, MemoryStream stream)
        {
            Connect();

            stream.Position = 0;
            database.GridFS.Delete(filename);
            var fsinfo = database.GridFS.Upload(stream, filename);
        }

        public static IEnumerable<string> GetAll()
        {
            Connect();
            return database.GridFS.FindAll().Select(x => x.Name);
        }

        private static void Connect()
        {
            if (server == null)
            {
                server = MongoServer.Create(ConfigurationManager.AppSettings.Get("MONGOLAB_URI"));
                database = server.GetDatabase(ConfigurationManager.AppSettings.Get("MONGOLAB_DB")); 
            }
        }
    }
}
