using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
[System.Web.Script.Services.ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class WebService : System.Web.Services.WebService
{
    public int test;
    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetShopsByMallIDJSON(string id)
    {
        //get the Json filepath  
        string file = Server.MapPath("~/shop.json");
        //deserialize JSON from file  
        string Json = System.IO.File.ReadAllText(file);
        // JavaScriptSerializer ser = new JavaScriptSerializer();
        // var list = ser.Deserialize<List<Object>>(Json);
        Context.Response.Write(Json);
    }
    public Point getI_J(int id)
    {
        string file = Server.MapPath("~/shop.json");
        int StoreI = 0, StoreJ = 0;
        string Json = System.IO.File.ReadAllText(file);
        JavaScriptSerializer jss = new JavaScriptSerializer();
        List<shop> shoplist = jss.Deserialize<List<shop>>(Json);
        for (var i = 0; i < shoplist.Count; i++)
        {
            if (shoplist[i].id == id)
            {
                StoreI = shoplist[i].i;
                StoreJ = shoplist[i].j;
            }
        }
        Point IJ = new Point();
        IJ.X = StoreI;
        IJ.Y = StoreJ;
        if ((StoreI == 0) ||(StoreJ == 0))
            Console.WriteLine("Wrong store name");
        return IJ;
    }
    public void drawMap(List<Point> optimalPath1, string PicName)
    {
        Bitmap originalBmp = new Bitmap(Server.MapPath("~/images/" + PicName + ".png"));
       // Bitmap originalBmp = new Bitmap(Image.FromFile(@"C:\\Users\\USER\\Desktop\\New folder\\" + PicName));

        // Create a blank bitmap with the same dimensions
        Bitmap btm = new Bitmap(originalBmp.Width, originalBmp.Height);

        // From this bitmap, the graphics can be obtained, because it has the right PixelFormat
        using(Graphics g = Graphics.FromImage(btm)){
            g.DrawImage(originalBmp, 0, 0);
        //Graphics g = Graphics.FromImage(btm);
        // Create graphics object for alteration.
        //Graphics g = Graphics.FromImage(btm);
        float x1, y1, x2, y2;
        for (int i = 0; i < optimalPath1.Count - 1; i++)
        {
            y1 = Convert.ToInt32(optimalPath1[i].Y * (btm.Width / 40)) + Convert.ToInt32((btm.Width / 40) * 6);
            x1 = Convert.ToInt32(optimalPath1[i].X * (btm.Height / 57)) + Convert.ToInt32((btm.Height /57) * 8.5);
            y2 = Convert.ToInt32(optimalPath1[i + 1].Y * (btm.Width / 40)) + Convert.ToInt32((btm.Width / 40) * 6);
            x2 = Convert.ToInt32(optimalPath1[i + 1].X * (btm.Height / 57)) + Convert.ToInt32((btm.Height / 57) * 8.5);

            g.DrawLine(new Pen(Color.Red,10), y1, x1, y2, x2);
        }
        float boxWidth = btm.Width / 24;
        // int rows = 45;
        // int clo = 39;
        }
        MemoryStream mem = new MemoryStream();
        String savePath = PicName + "1.png";
        btm.Save(Server.MapPath("~/images/" + savePath), ImageFormat.Png);
        //return savePath;
    }
    //public string[] optimalPathPic;
    public List<string> optimalPathPic = new List<string>();
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void  GetMapByShopsJSON(int id1, int id2)
    {

        Point storeCurr, storeDest;
        storeCurr = getI_J(id1);
        storeDest = getI_J(id2);
      
        brain b = new brain();

        optimalPathPic = b.checkFloor(id1, storeCurr.X, storeCurr.Y, id2, storeDest.X, storeDest.Y);
      Context.Response.Write(String.Join(",", optimalPathPic.ToArray()));
    }
}


