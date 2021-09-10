using Microsoft.Data.SqlClient;
using Roomates.Models;
using System.Collections.Generic;

namespace Roomates.Repositories
{

    public class ChoreRepository : BaseRepository
    {
        public ChoreRepository(string connectionString) : base(connectionString) { }

            public List<Chore> GetAll()
                {
                using (SqlConnection conn = Connection) { 
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Id, Name FROM Chore";
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<Chore> chores = new List<Chore>();

                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int idValue = reader.GetInt32(idColumnPosition);
                            int nameColumnPosition = reader.GetOrdinal("Name");
                            string nameValue = reader.GetString(nameColumnPosition);

                            Chore chore = new Chore
                            {
                                Id = idValue,
                                Name = nameValue,
                            };

                            chores.Add(chore);
                        }
                        reader.Close();
                        return chores;
                    }
                }
                
            
             }

        public Chore GetById (int id)

    
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd =conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Chore chore = null;

                    if (reader.Read())
                    {
                        chore = new Chore
                        {
                            Id = id,
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                    }
                    reader.Close();
                    return chore;
                }
            }
        }

        public void Insert(Chore chore)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Chore (NAME) OUTPUT INSERTED.Id VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    int id = (int)cmd.ExecuteScalar();

                    chore.Id = id;
                    
                }
            }
        }
    }
}
                   

                
            
        
    

