﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repository
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetrics>
    {

    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;" +
                                                "Pooling=true;Max Pool Size=100";

        

        public IList<DotNetMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<DotNetMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }

            return returnList;
        }

        public DotNetMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM cpumetrics where id=@id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public void Create(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "Insert into cpumetrics(value, time) Values" +
                              "(@value,@time)"
            };
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public void Update(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "Update cpumetrics Set value = @value, time = @time Where id=@id"
            };
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection)
            {
                CommandText = "Delete from cpumetrics where id=@id"
            };
            cmd.Parameters.AddWithValue("@id", id);
            
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}
