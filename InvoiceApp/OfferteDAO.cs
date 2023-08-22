using iTextSharp.text.xml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace InvoiceApp
{
    internal class OfferteDAO
    {

        static string info = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\", "OfferteDatabase.mdf"));

        string infoConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                    $"AttachDbFilename={info};" +
                                    "Integrated Security=True;" +
                                    "Connect Timeout=30";
        public List<Offerte> searchTitel(String searchTherm)
        {
            List<Offerte> returnThese = new List<Offerte>();

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();

            String searchWildPhrase = "%" + searchTherm + "%";

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Offertes where TitelOfferte like @search";
            cmd.Parameters.AddWithValue("@search", searchWildPhrase);
            cmd.Connection = connection;

            

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                   
                        returnThese.Add(new Offerte()
                        {
                            Id = reader.GetInt32(0),
                            BedrijfNaam = reader.GetString(1),
                            KlantBedrijfNaam = reader.GetString(2),
                            TitelOfferte = reader.GetString(3),
                            TotaalBedrag = Convert.ToSingle( reader.GetDouble(4)),
                           
                        });

                    
                }
            }
            connection.Close();

            return returnThese;
        }

        public List<Bedrijf> GetBedrijfForOfferte(int Bedrijf_Id)
        {
            List<Bedrijf> returnThese = new List<Bedrijf>();

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();

            
            

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Bedrijven where BedrijfId = @BedrijfId";
            cmd.Parameters.AddWithValue("@BedrijfId", Bedrijf_Id);
            cmd.Connection = connection;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Bedrijf()
                    {
                        BedrijfId = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        NrTva = reader.GetInt32(2),
                        Adres = reader.GetString(3),
                        Email = reader.GetString(4),
                        NrTel = reader.GetInt32(5)
                    });


                }
            }
            connection.Close();

            return returnThese;
        }

        public List<Offerte> ToonBigOffertes(float Totaal_Bedrag)
        {
            List<Offerte> returnThese = new List<Offerte>();

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();

            

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Offertes";
            cmd.Parameters.AddWithValue("@TotaalBedrag", Totaal_Bedrag);

            cmd.Connection = connection;

            

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Offerte()
                    {
                        Id = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        KlantBedrijfNaam = reader.GetString(2),
                        TitelOfferte = reader.GetString(3),
                        TotaalBedrag = Convert.ToSingle(reader.GetDouble(4)),

                    });


                }
            }

            var GroterDanOrderByDesc =
            from x in returnThese
            where x.TotaalBedrag > Totaal_Bedrag
            orderby x.TotaalBedrag descending
            select x;

            var lijst = GroterDanOrderByDesc.ToList();

            connection.Close();

            return lijst;
        }


        public List<Bedrijf> searchBedrijfNaam(String searchTherm)
        {
            List<Bedrijf> returnThese = new List<Bedrijf>();

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();

            String searchWildPhrase = "%" + searchTherm + "%";

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Bedrijven where BedrijfNaam like @search";
            cmd.Parameters.AddWithValue("@search", searchWildPhrase);
            cmd.Connection = connection;



            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Bedrijf()
                    {
                        BedrijfId = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        NrTva = reader.GetInt32(2),
                        Adres = reader.GetString(3),
                        Email = reader.GetString(4),
                        NrTel = reader.GetInt32(5)

                    });


                }
            }
            connection.Close();

            return returnThese;
        }

        public List<Bedrijf> searchKlantenNaam(String searchTherm)
        {
           
            List<Bedrijf> returnThese = new List<Bedrijf>();

            

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();

            String searchWildPhrase = "%" + searchTherm + "%";

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Klanten where BedrijfNaam like @search";
            cmd.Parameters.AddWithValue("@search", searchWildPhrase);
            cmd.Connection = connection;

            

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Bedrijf()
                    {
                        BedrijfId = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        NrTva = reader.GetInt32(2),
                        Adres = reader.GetString(3),
                        Email = reader.GetString(4),
                        NrTel = reader.GetInt32(5)

                    });


                }
            }
            connection.Close();

            return returnThese;
        }

        internal int deleteBedrijf(int BedrijfId)
        {
            SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();

            SqlCommand cmd = new SqlCommand("delete from Bedrijven where Bedrijven.BedrijfId = @BedrijfId");
            cmd.Parameters.AddWithValue("@BedrijfId", BedrijfId);
            cmd.Connection = connection;

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return result;
        }

        internal int deleteKlant(int KlantId)
        {
            SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();

            SqlCommand cmd = new SqlCommand("delete from Klanten where Klanten.KlantId = @KlantId");
            cmd.Parameters.AddWithValue("@KlantId", KlantId);
            cmd.Connection = connection;

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return result;
        }

        internal int deleteOfferte(int OfferteId)
        {
            SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();

            SqlCommand cmd = new SqlCommand("delete from Offertes where Offertes.Id = @OfferteId");
            cmd.Parameters.AddWithValue("@OfferteId", OfferteId);
            cmd.Connection = connection;

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return result;
        }

        public float ToonGrootsteOfferte()
        {
            List<Offerte> returnThese = new List<Offerte>();
            

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();



            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Offertes";
            
            cmd.Connection = connection;



            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Offerte()
                    {
                        Id = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        KlantBedrijfNaam = reader.GetString(2),
                        TitelOfferte = reader.GetString(3),
                        TotaalBedrag = Convert.ToSingle(reader.GetDouble(4)),
                       

                    });


                }
            }

            
            float GrootsteBedrag = returnThese.Max(m => m.TotaalBedrag);
            
            
           

            connection.Close();

            return GrootsteBedrag;
        }

        public List<Bedrijf> OrderByBedrijfNaamDesc()
        {
            List<Bedrijf> returnThese = new List<Bedrijf>();

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();



            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Bedrijven";
            

            cmd.Connection = connection;



            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Bedrijf()
                    {
                        BedrijfId = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        NrTva = reader.GetInt32(2),
                        Adres = reader.GetString(3),
                        Email = reader.GetString(4),
                        NrTel = reader.GetInt32(5)

                    });


                }
            }

            var orderlist =
            from x in returnThese
            orderby x.BedrijfNaam descending
            select x;

            var lijst = orderlist.ToList();

            connection.Close();

            return lijst;
        }

        public List<Bedrijf> OrderByBedrijfNaamAsc()
        {
            List<Bedrijf> returnThese = new List<Bedrijf>();

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();



            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Bedrijven";


            cmd.Connection = connection;



            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Bedrijf()
                    {
                        BedrijfId = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        NrTva = reader.GetInt32(2),
                        Adres = reader.GetString(3),
                        Email = reader.GetString(4),
                        NrTel = reader.GetInt32(5)

                    });


                }
            }

            var orderlist =
            from x in returnThese
            orderby x.BedrijfNaam ascending
            select x;

            var lijst = orderlist.ToList();

            connection.Close();

            return lijst;
        }

        public List<Klant> OrderByKlantBedrijfNaamDesc()
        {
            List<Klant> returnThese = new List<Klant>();

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();



            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Klanten";


            cmd.Connection = connection;



            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Klant()
                    {
                        KlantId = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        NrTva = reader.GetInt32(2),
                        Adres = reader.GetString(3),
                        Email = reader.GetString(4),
                        NrTel = reader.GetInt32(5)

                    });


                }
            }

            var orderlist =
            from x in returnThese
            orderby x.BedrijfNaam descending
            select x;

            var lijst = orderlist.ToList();

            connection.Close();

            return lijst;
        }

        public List<Klant> OrderByKlantBedrijfNaamAsc()
        {
            List<Klant> returnThese = new List<Klant>();

            using SqlConnection connection = new SqlConnection(infoConnection);

            connection.Open();



            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from Klanten";


            cmd.Connection = connection;



            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    returnThese.Add(new Klant()
                    {
                        KlantId = reader.GetInt32(0),
                        BedrijfNaam = reader.GetString(1),
                        NrTva = reader.GetInt32(2),
                        Adres = reader.GetString(3),
                        Email = reader.GetString(4),
                        NrTel = reader.GetInt32(5)

                    });


                }
            }

            var orderlist =
            from x in returnThese
            orderby x.BedrijfNaam ascending
            select x;

            var lijst = orderlist.ToList();

            connection.Close();

            return lijst;
        }


    }
}
