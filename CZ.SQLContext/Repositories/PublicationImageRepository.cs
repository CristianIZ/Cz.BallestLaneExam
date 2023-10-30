using CZ.Domain;
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
    public class PublicationImageRepository : IPublicationImageRepository
    {
        public Task Add(PublicationImage publication)
        {
            string query = $"INSERT INTO [dbo].[Publication] ([Key], [UserId], [Title], [Date], [PhotoContent], [Base64Image]) VALUES (@Key, @UserId, @Title, @Date, @PhotoContent, @Base64Image)";
            var sqlParameters = new ArrayList();

            publication.Key = Guid.NewGuid().ToString();

            sqlParameters.Add(SqlHelper.CreateParameter("Key", publication.Key));
            sqlParameters.Add(SqlHelper.CreateParameter("UserId", publication.UserId));
            sqlParameters.Add(SqlHelper.CreateParameter("Title", publication.Title));
            sqlParameters.Add(SqlHelper.CreateParameter("Date", publication.Date));
            sqlParameters.Add(SqlHelper.CreateParameter("PhotoContent", publication.PhotoContent));
            sqlParameters.Add(SqlHelper.CreateParameter("Base64Image", publication.Base64Image));

            using (var DA = new SQLDataAccess())
            {
                DA.ExecuteQuery(query, sqlParameters);
            }

            return Task.CompletedTask;
        }

        public Task<PublicationImage> GetByKey(string key)
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

        public ICollection<PublicationImage> ReadEntity(DataTable dataTable)
        {
            ICollection<PublicationImage> publication = new List<PublicationImage>();

            foreach (DataRow item in dataTable.Rows)
            {
                publication.Add(MapEntity(item));
            }

            return publication;
        }

        public PublicationImage MapEntity(DataRow dataRow)
        {
            return new PublicationImage()
            {
                Id = Convert.ToInt32(dataRow["Id"]),
                Key = dataRow["Key"].ToString(),
                UserId = Convert.ToInt32(dataRow["UserId"]),
                Base64Image = dataRow["Base64Image"].ToString(),
                Date = Convert.ToDateTime(dataRow["Date"]),
                PhotoContent = dataRow["PhotoContent"].ToString(),
                Title = dataRow["Title"].ToString(),
            };
        }
    }
}
