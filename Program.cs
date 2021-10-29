using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using IronXL;

namespace NBA_Stat_Request
{
    class Program
    {
        static HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
        };
        static readonly HttpClient client = new HttpClient(handler);

        static Depot rData;

        static async Task Main()
        {
            

            await GetStats();
            // WriteHeaders();
            WriteStats();

            //foreach(string header in rData.resultSets[0].headers)
            //{
            //    Console.WriteLine(header);
            //}
            
            Console.Read();
          
        }

        static async Task GetStats()
        {
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/plain,");
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add("Origin", "https://www.nba.com");
            client.DefaultRequestHeaders.Add("Referer", "https://www.nba.com/");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://stats.nba.com/stats/leaguegamefinder?Conference=&DateFrom=&DateTo=&Division=&DraftNumber=&DraftRound=&DraftYear=&GB=N&LeagueID=00&Location=&Outcome=&PlayerID=406&PlayerOrTeam=P&Season=&SeasonType=&StatCategory=PTS&TeamID=&VsConference=&VsDivision=&VsTeamID=&gtPTS=40");
                var responseBody = await response.Content.ReadAsStringAsync();
                Depot stats = JsonConvert.DeserializeObject<Depot>(responseBody);
                rData = stats;

                Console.WriteLine(stats.resultSets[0].rowSet[1][0]);
                //Console.Read();                
                                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);                
                                
            }

        }

        static void WriteHeaders()
        {
            WorkBook firstBook = WorkBook.Load("ThirdExcelFile.xlsx");
            WorkSheet sheet = firstBook.DefaultWorkSheet;

            IList<string> headers = rData.resultSets[0].headers;

            for (int i = 3; i < headers.Count+3; i++)
            {
               sheet.SetCellValue(1, i, headers[i-3]);
            }
            
            firstBook.Save();
            Console.WriteLine("Headers Written");
        }

        static void WriteStats()
        {
            WorkBook firstBook = WorkBook.Load("ThirdExcelFile.xlsx");
            WorkSheet sheet = firstBook.DefaultWorkSheet;

            IList<IList<object>> data = rData.resultSets[0].rowSet;

            int col = 3;
            int row = 2;

            foreach (var statline in data)
            {
                foreach (var stat in statline)
                {
                    sheet.SetCellValue(row, col, stat);

                    //Console.WriteLine(stat);
                    col++;
                }
                col = 3;
                row++;
            }

            firstBook.Save();
            Console.WriteLine("Stats Written");
        }

         
        public void OldCode()
        {
            /*try
            {
                HttpResponseMessage response = await client.GetAsync("https://stats.nba.com/stats/leaguegamefinder?Conference=&DateFrom=&DateTo=&Division=&DraftNumber=&DraftRound=&DraftYear=&GB=N&LeagueID=00&Location=&Outcome=&PlayerID=406&PlayerOrTeam=P&Season=&SeasonType=&StatCategory=PTS&TeamID=&VsConference=&VsDivision=&VsTeamID=&gtPTS=40");
                //HttpResponseMessage response = await client.GetAsync("http://data.nba.net/10s/prod/v1/20170218/scoreboard.json");
                //response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                //string statRequest = ("https://stats.nba.com/stats/leaguegamefinder?Conference=&DateFrom=&DateTo=&Division=&DraftNumber=&DraftRound=&DraftYear=&GB=N&LeagueID=00&Location=&Outcome=&PlayerID=406&PlayerOrTeam=P&Season=&SeasonType=&StatCategory=PTS&TeamID=&VsConference=&VsDivision=&VsTeamID=&gtPTS=40");
                //HttpResponseMessage responseBody = await client.GetAsync(statRequest);

                //var stats = JsonConvert.DeserializeObject(responseBody);
                //DataSet stats = JsonConvert.DeserializeObject<DataSet>(responseBody);
                Depot stats = JsonConvert.DeserializeObject<Depot>(responseBody);
                //var tblHeaders = stats;


                //Console.WriteLine(response);
                //Console.WriteLine(stats);
                Console.WriteLine(stats.resultSets[0].rowSet[1][0]);
                //Console.WriteLine(response.Content.Headers);
                //Console.WriteLine(stats2);
                Console.Read();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                Console.Read();
            }
            */

                    /*
                        * The currency converter is a really good parallell for this; the data table
                        * could play a role as temporary storage for testing
                        * 
                        * Have to maunually create the class structure to map the json response
                        * 
                        * Then have to hopefully use a loop to create the data table headers 
                        * and fill the rows
                        * 
            */
        }

    }
}
