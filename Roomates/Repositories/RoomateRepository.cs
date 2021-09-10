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
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, RentPortion, MovedInDate, Room FROM Roomate rm LEFT JOIN Room r on rm.RoomId = r.id";
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

                        int rentPortionPosition = reader.GetOrdinal("Id");
                        int rentPortion = reader.GetInt32(rentPortionPosition);

                        int moveInDatePosition = reader.GetOrdinal("MoveInDate");
                         var moveInDate = reader.GetDateTime(moveInDatePosition);

                        int roomNamePosition = reader.GetOrdinal("r.Name");
                        string roomName = reader.GetString(roomNamePosition);

                        int idRoomCollumnPosition = reader.GetOrdinal("Id");
                        int roomId = reader.GetInt16(idRoomCollumnPosition);

                        int MaxOccupancyPosition = reader.GetOrdinal("r.MaxOccupancy");
                        int maxOcc = reader.GetInt16(MaxOccupancyPosition);
                        




                        Roomate roomate = new Roomate
                        {
                            Id = idValue,
                            FirstName = firstName,
                            LastName = Lastname,
                            RentPortion = rentPortion,
                            MovedInDate = moveInDate,
                            Room = new Room
                            {
                                Id = roomId,
                                Name = roomName,  
                                MaxOccupancy = maxOcc,
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
