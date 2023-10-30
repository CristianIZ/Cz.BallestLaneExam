using CZ.Domain.PublicationManagement;
using CZ.SQLContext.Helpers;
using CZ.SQLContext.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.SQLContext.Repositories
{
    public class PublicationTextRepository : IPublicationTextRepository
    {
        public Task Add(PublicationText publication)
        {
            string query = $"INSERT INTO [dbo].[Publication] ([Key], [UserId], [Title], [Date], [Text]) VALUES (@Key, @UserId, @Title, @Date, @Text)";
            var sqlParameters = new ArrayList();

            publication.Key = Guid.NewGuid().ToString();

            sqlParameters.Add(SqlHelper.CreateParameter("Key", publication.Key));
            sqlParameters.Add(SqlHelper.CreateParameter("UserId", publication.UserId));
            sqlParameters.Add(SqlHelper.CreateParameter("Title", publication.Title));
            sqlParameters.Add(SqlHelper.CreateParameter("Date", publication.Date));
            sqlParameters.Add(SqlHelper.CreateParameter("Text", publication.Text));

            using (var DA = new SQLDataAccess())
            {
                DA.ExecuteQuery(query, sqlParameters);
            }

            return Task.CompletedTask;
        }

        public Task<PublicationText> GetByKey(string key)
        {
            string query = $"SELECT * FROM [dbo].[Publication] WHERE [Key] = @key";

            using (var DA = new SQLDataAccess())
            {
                var sqlParameters = new ArrayList();
                sqlParameters.Add(SqlHelper.CreateParameter("Key", key));

                var table = DA.Read(query, sqlParameters);
                return Task.FromResult(ReadEntity(table).First());
            }
        }

        public ICollection<PublicationText> ReadEntity(DataTable dataTable)
        {
            ICollection<PublicationText> publication = new List<PublicationText>();

            foreach (DataRow item in dataTable.Rows)
            {
                publication.Add(MapEntity(item));
            }

            return publication;
        }

        public PublicationText MapEntity(DataRow dataRow)
        {
            return new PublicationText()
            {
                Id = Convert.ToInt32(dataRow["Id"]),
                Key = dataRow["Key"].ToString(),
                UserId = Convert.ToInt32(dataRow["UserId"]),
                Date = Convert.ToDateTime(dataRow["Date"]),
                Title = dataRow["Title"].ToString(),
                Text = dataRow["Text"].ToString(),
            };
        }
    }
}
