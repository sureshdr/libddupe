using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace sureshdr.libddupe
{
    public class DataMgmt : IDisposable
    {

        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public bool IsInitialised = false;
        public SQLiteConnection dbConn;
        public bool IsExistDrive(string SerialNo)
        {
            bool result = false;

            SQLiteCommand cmd = new SQLiteCommand(dbConn);
            cmd.CommandText = "SELECT COUNT(*) From DEVICES where device_serial = @Serial";
            cmd.Parameters.AddWithValue("@Serial", SerialNo);
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0)
                result = true;
            return result;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                dbConn.Close();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        public void AddDrive(HardDrive hd)
        {
            SQLiteCommand cmd = new SQLiteCommand(dbConn);
            cmd.CommandText = @"INSERT INTO Devices(device_id, device_serial, device_model, device_type) 
                VALUES(@ID, @Serial, @Model, @Type)";
            cmd.Parameters.AddWithValue("@ID", hd.Guid);
            cmd.Parameters.AddWithValue("@Serial", hd.SerialNo);
            cmd.Parameters.AddWithValue("@Model", hd.Model);
            cmd.Parameters.AddWithValue("@Type", hd.Type);
            cmd.ExecuteNonQuery();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DataMgmt(string dbpath)
        {
            IsInitialised = true;
            dbConn = new SQLiteConnection(
                "DataSource=" + dbpath + ";"
                +"Version=3");
            dbConn.Open();
        }

        ~DataMgmt()
        {
            dbConn.Close();
        }
        
    }
}
