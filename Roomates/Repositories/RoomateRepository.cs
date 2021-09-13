using Microsoft.Data.SqlClient;
using Roomates.Models;
using System.Collections.Generic;
using System;

namespace Roomates.Repositories
{
    public class RoomateRepository : BaseRepository
    {
        public RoomateRepository(string connectionString) : base(connectionString) { }


        public List<Roomate> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT rm.Id, rm.FirstName, rm.LastName, rm.RentPortion, rm.MovedInDate,r.MaxOccupancy,r.Id, r.Name  FROM Roomate rm JOIN Room r ON  rm.roomId = r.Id";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roomate> roomates = new List<Roomate>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstName = reader.GetString(firstNameColumnPosition);

                        int lastnameColumnPosition = reader.GetOrdinal("LastName");
                        string Lastname = reader.GetString(lastnameColumnPosition);

                        int rentPortionPosition = reader.GetOrdinal("RentPortion");
                        int rentPortion = reader.GetInt32(rentPortionPosition);

                        int moveInDatePosition = reader.GetOrdinal("MoveInDate");
                         var moveInDate = reader.GetDateTime(moveInDatePosition);

                         int roomIdPosition = reader.GetOrdinal("Id");
                         int roomId = reader.GetInt32(roomIdPosition);

                         int maxOccPosition = reader.GetOrdinal("MaxOccupancy");
                         int maxOcc = reader.GetInt32(roomIdPosition);

                         int roomNamePosition = reader.GetOrdinal("Name");
                         string roomName = reader.GetString(roomNamePosition);

                       

                        Roomate roomate = new Roomate
                        {
                            Id = idValue,
                            FirstName = firstName,
                            LastName = Lastname,
                            RentPortion = rentPortion,
                            MovedInDate = moveInDate,
                            Room = new Room(){
                                Id = roomId,
                                Name = roomName,
                                MaxOccupancy = maxOcc
                            }


                        };

                        roomates.Add(roomate);
                    }
                    reader.Close();

                    return roomates;
                }
            }
        }

    }
}
