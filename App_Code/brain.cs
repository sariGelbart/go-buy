using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.IO;

/// <summary>
/// Summary description for brain
/// </summary>
public class brain
{
    map map = new map();
    public class node
    {
        Point p;

        public Point P
        {
            get { return p; }
            set { p = value; }
        }
        int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public node(Point p, int Count)
        {
            this.p = p;
            this.Count = Count;
        }

        public node()
        {
            // TODO: Complete member initialization
        }

    }
    public void ClearMat(int[,] mat)
    {
        for (int i = 0; i <= 32; i++)
            for (int j = 0; j <= 24; j++)
                if (mat[i, j] == -1)
                    mat[i, j] = 2;
    }

    List<Point> OptimalPath = new List<Point>();
    //OptimalPath.RemoveAll();
    Point p = new Point();
    node lastnode = new node();
    Stack<node> nodes = new Stack<node>();
    map map1 = new map();
    int countSteps = 1;
    int escalatorA_i, escalatorA_j, escalatorA_id, escalatorB_i, escalatorB_j, escalatorB_id, escalatorUp_id, escalatorUp_i, escalatorUp_j;
    List<Point> Path1 = new List<Point>();
    List<Point> Path2 = new List<Point>();
    List<Point> test = new List<Point>();
      List<Point> Path3 = new List<Point>();
      List<Point> Path4 = new List<Point>();
          List<Point>Path5= new List<Point>();
    List<Point> Path6 = new List<Point>();
    int way1steps, way2steps, way3steps;
    WebService wb = new WebService();
    Point EscalatorIJ = new Point();
    Point EscalatorIJ2 = new Point();
    string PicFloor1 = "PicFloor1" , PicFloor2 = "PicFloor2",PicFloor3 = "PicFloor3", PicFloor4 ="PicFloor4";
    List <string> PicNames = new List<string>();
    string pic = "PicFloor";
    public List<string> checkFloor(int StoreID_current, int i_current, int j_current, int StoreID_destination, int i_destination, int j_destination)
    {
        int elavator2 = 278, escalator3Down = 346, escalator2UpDown = 276, elavator4 = 446, escalator1up = 107, escalator4Down1 = 447, escalator4Down2 = 448, elavator3 = 349, escalator3UpDown = 350,escalator3Up = 360, escalator2Up = 275;

        //אם החנות באותה קומה
        if ((StoreID_current / 100) == (StoreID_destination / 100))
        {
            Path1 = CalcPath(StoreID_current, i_current, j_current, StoreID_destination, i_destination, j_destination);
            pic = pic + (StoreID_current / 100);
            wb.drawMap(Path1, pic);
            PicNames.Add(pic + "1.png");
        }
        // אם החנות לא באותה קומה
        else
        {
            //בדיקה אם אני בקומה הראשונה ואז יש רק מדרגות נעות לעלות
            if ((StoreID_current / 100) == 1)
            {
                WebService wb = new WebService();
                EscalatorIJ = wb.getI_J(escalator1up);
                // EscalatorIJ = getI_J("escalator1Up");
                // בידקת הI וJ של המדרגות הנעות
                escalatorA_i = EscalatorIJ.X;
                escalatorA_j = EscalatorIJ.Y;
                Path1 = CalcPath(StoreID_current, i_current, j_current, escalator1up, escalatorA_i, escalatorA_j);
                wb.drawMap(Path1, PicFloor1);
                PicNames.Add(PicFloor1 + "1.png");

                if (((StoreID_destination / 100) == 2) || ((StoreID_destination / 100) == 3))
                {
                    if ((StoreID_destination / 100) == 2)
                    {
                        EscalatorIJ = wb.getI_J(escalator2UpDown);
                        escalatorA_id = escalator2UpDown;
                    }
                    else
                    {
                        ////לבדוק את הדרך מהמדרגות נעות ליעד                      
                        EscalatorIJ = wb.getI_J(escalator3Down);
                        escalatorA_id = escalator3Down;
                    }
                    escalatorA_i = EscalatorIJ.X;
                    escalatorA_j = EscalatorIJ.Y;
                    Path2 = CalcPath(escalatorA_id, escalatorA_i, escalatorA_j, StoreID_destination, i_destination, j_destination);
                    wb.drawMap(Path1, pic + (StoreID_destination / 100));
                    PicNames.Add(pic + (StoreID_destination / 100) + "1.png");
                }
                //היעד בקומה 4
                else
                {
                    EscalatorIJ = wb.getI_J(escalator2UpDown);
                    escalatorA_id = escalator2UpDown;
                    escalatorA_i = EscalatorIJ.X;
                    escalatorA_j = EscalatorIJ.Y;


                    EscalatorIJ = wb.getI_J(elavator2);
                    escalatorB_id = elavator2;
                    escalatorB_i = EscalatorIJ.X;
                    escalatorB_j = EscalatorIJ.Y;
                    Path2 = CalcPath(escalatorA_id, escalatorA_i, escalatorA_j, escalatorB_id, escalatorB_i, escalatorB_j);
                    wb.drawMap(Path1, PicFloor2);
                    PicNames.Add(PicFloor2 + "1.png");


                    EscalatorIJ = wb.getI_J(elavator4);
                    escalatorA_id = elavator4;
                    escalatorA_i = EscalatorIJ.X;
                    escalatorA_j = EscalatorIJ.Y;
                    Path3 = CalcPath(escalatorA_id, escalatorA_i, escalatorA_j, StoreID_destination, i_destination, j_destination);
                    wb.drawMap(Path1, PicFloor4);
                    PicNames.Add(PicFloor4 + "1.png");
                }
            }
            //אם אני בקומה 4
            else if ((StoreID_current / 100) == 4)
            {
                if ((StoreID_destination / 100) == 1)
                {
                    EscalatorIJ = wb.getI_J(elavator4);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, elavator4, EscalatorIJ.X, EscalatorIJ.Y);
                    wb.drawMap(Path1, PicFloor4);
                    PicNames.Add(PicFloor4 + "1.png");

                    EscalatorIJ = wb.getI_J(elavator2);
                    EscalatorIJ2 = wb.getI_J(escalator2UpDown);
                    Path1 = CalcPath(elavator2, EscalatorIJ.X, EscalatorIJ.Y, escalator2UpDown, EscalatorIJ2.X, EscalatorIJ2.Y);
                    wb.drawMap(Path1, PicFloor2);
                    PicNames.Add(PicFloor2 + "1.png");

                    EscalatorIJ = wb.getI_J(escalator1up);
                    Path2 = CalcPath(escalator1up, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    wb.drawMap(Path2, PicFloor1);
                    PicNames.Add(PicFloor1 + "1.png");
                }
                else if ((StoreID_destination / 100) == 2)
                {
                    EscalatorIJ = wb.getI_J(elavator4);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, elavator4, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(elavator2);
                    Path2 = CalcPath(elavator2, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way1steps = Path1.Count + Path2.Count;

                    EscalatorIJ = wb.getI_J(escalator4Down1);
                    Path3 = CalcPath(StoreID_current, i_current, j_current, escalator4Down1, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator2UpDown);
                    Path4 = CalcPath(escalator2UpDown, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way2steps = Path3.Count + Path4.Count;
                    if (way1steps > way2steps)
                    {
                        wb.drawMap(Path3, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                        wb.drawMap(Path4, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                    }
                    else
                    {
                        wb.drawMap(Path1, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                        wb.drawMap(Path2, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                    }
                }
                //לקומה 3
                else
                {
                    EscalatorIJ = wb.getI_J(elavator4);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, elavator4, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(elavator3);
                    Path2 = CalcPath(elavator3, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way1steps = Path1.Count + Path2.Count;

                    EscalatorIJ = wb.getI_J(escalator4Down1);
                    Path3 = CalcPath(StoreID_current, i_current, j_current, escalator4Down1, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator3UpDown);
                    Path4 = CalcPath(escalator3UpDown, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way2steps = Path3.Count + Path4.Count;
                    if (way1steps > way2steps)
                    {
                        wb.drawMap(Path3, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                        wb.drawMap(Path4, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                    }
                    else
                    {
                        wb.drawMap(Path1, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                        wb.drawMap(Path2, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                    }
                }
            }
            //אם אני בקומה 2
            else if ((StoreID_current / 100) == 2)
            {
                //לקומה 1
                if ((StoreID_destination / 100) == 1)
                {
                    EscalatorIJ = wb.getI_J(escalator2UpDown);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, escalator2UpDown, EscalatorIJ.X, EscalatorIJ.Y);
                    wb.drawMap(Path1, PicFloor2);
                    PicNames.Add(PicFloor2 + "1.png");
                    EscalatorIJ = wb.getI_J(escalator1up);
                    Path2 = CalcPath(escalator1up, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    wb.drawMap(Path2, PicFloor1);
                    PicNames.Add(PicFloor1 + "1.png");
                }
                else if ((StoreID_destination / 100) == 3)
                {
                    EscalatorIJ = wb.getI_J(escalator2UpDown);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, escalator2UpDown, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator3Down);
                    Path2 = CalcPath(escalator3Down, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way1steps = Path1.Count + Path2.Count;

                    EscalatorIJ = wb.getI_J(elavator2);
                    Path3 = CalcPath(StoreID_current, i_current, j_current, elavator2, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(elavator3);
                    Path4 = CalcPath(elavator3, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way2steps = Path3.Count + Path4.Count;

                    EscalatorIJ = wb.getI_J(escalator2Up);
                    Path5 = CalcPath(StoreID_current, i_current, j_current, escalator2Up, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator3UpDown);
                    Path6 = CalcPath(escalator3UpDown, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way3steps = Path5.Count + Path6.Count;

                    if ((way1steps < way2steps) && (way1steps < way3steps))
                    {
                        wb.drawMap(Path1, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                        wb.drawMap(Path2, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                    }
                    else if ((way2steps < way1steps) && (way2steps < way3steps))
                    {
                        wb.drawMap(Path3, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                        wb.drawMap(Path4, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                    }
                    else if ((way3steps < way1steps) && (way3steps < way2steps))
                    {
                        wb.drawMap(Path5, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                        wb.drawMap(Path6, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                    }
                }
                else if ((StoreID_destination / 100) == 4)
                {
                    EscalatorIJ = wb.getI_J(elavator2);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, elavator2, EscalatorIJ.X, EscalatorIJ.Y);
                    wb.drawMap(Path1, PicFloor2);
                    PicNames.Add(PicFloor2 + "1.png");
                    EscalatorIJ = wb.getI_J(elavator4);
                    Path2 = CalcPath(elavator4, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    wb.drawMap(Path2, PicFloor4);
                    PicNames.Add(PicFloor4 + "1.png");
                    way1steps = Path1.Count + Path2.Count;

                    EscalatorIJ = wb.getI_J(escalator2Up);
                    Path3 = CalcPath(StoreID_current, i_current, j_current, escalator2Up, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator4Down1);
                    Path4 = CalcPath(escalator4Down1, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way2steps = Path3.Count + Path4.Count;

                    if (way1steps > way2steps)
                    {
                        wb.drawMap(Path3, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                        wb.drawMap(Path4, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                    }
                    else
                    {
                        wb.drawMap(Path1, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                        wb.drawMap(Path2, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                    }
                }
            }
            //אם אני בקומה 3
            else if ((StoreID_current / 100) == 3)
            {
                //לקומה 1
                if ((StoreID_destination / 100) == 1)
                {
                    EscalatorIJ = wb.getI_J(escalator3Down);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, escalator3Down, EscalatorIJ.X, EscalatorIJ.Y);
                    wb.drawMap(Path1, PicFloor3);
                    PicNames.Add(PicFloor4 + "1.png");
                    EscalatorIJ = wb.getI_J(escalator1up);
                    Path2 = CalcPath(escalator1up, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    wb.drawMap(Path2, PicFloor1);
                    PicNames.Add(PicFloor4 + "1.png");

                }
                else if ((StoreID_destination / 100) == 2)
                {
                    EscalatorIJ = wb.getI_J(escalator3Down);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, escalator3Down, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator2UpDown);
                    Path2 = CalcPath(escalator2UpDown, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way1steps = Path1.Count + Path2.Count;

                    EscalatorIJ = wb.getI_J(escalator3UpDown);
                    Path3 = CalcPath(StoreID_current, i_current, j_current, escalator3UpDown, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator2Up);
                    Path4 = CalcPath(escalator2Up, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way2steps = Path3.Count + Path4.Count;

                    EscalatorIJ = wb.getI_J(elavator3);
                    Path5 = CalcPath(StoreID_current, i_current, j_current, elavator3, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(elavator2);
                    Path6 = CalcPath(elavator2, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way3steps = Path5.Count + Path6.Count;

                    if ((way1steps < way2steps) && (way1steps < way3steps))
                    {
                        wb.drawMap(Path1, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                        wb.drawMap(Path2, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                    }
                    else if ((way2steps < way1steps) && (way2steps < way3steps))
                    {
                        wb.drawMap(Path3, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                        wb.drawMap(Path4, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                    }
                    else if ((way3steps < way1steps) && (way3steps < way2steps))
                    {
                        wb.drawMap(Path5, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                        wb.drawMap(Path6, PicFloor2);
                        PicNames.Add(PicFloor2 + "1.png");
                    }

                }
                else if ((StoreID_destination / 100) == 4)
                {
                    EscalatorIJ = wb.getI_J(escalator3UpDown);
                    Path1 = CalcPath(StoreID_current, i_current, j_current, escalator3UpDown, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator4Down1);
                    Path2 = CalcPath(escalator4Down1, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way1steps = Path1.Count + Path2.Count;

                    EscalatorIJ = wb.getI_J(escalator3Up);
                    Path3 = CalcPath(StoreID_current, i_current, j_current, escalator3Up, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(escalator4Down2);
                    Path4 = CalcPath(escalator4Down2, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way2steps = Path3.Count + Path4.Count;

                    EscalatorIJ = wb.getI_J(elavator3);
                    Path5 = CalcPath(StoreID_current, i_current, j_current, elavator3, EscalatorIJ.X, EscalatorIJ.Y);
                    EscalatorIJ = wb.getI_J(elavator4);
                    Path6 = CalcPath(elavator4, EscalatorIJ.X, EscalatorIJ.Y, StoreID_destination, i_destination, j_destination);
                    way3steps = Path5.Count + Path6.Count;

                    if ((way1steps < way2steps) && (way1steps < way3steps))
                    {
                        wb.drawMap(Path1, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                        wb.drawMap(Path2, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                    }
                    else if ((way2steps < way1steps) && (way2steps < way3steps))
                    {
                        wb.drawMap(Path3, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                        wb.drawMap(Path4, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                    }
                    else if ((way3steps < way1steps) && (way3steps < way2steps))
                    {
                        wb.drawMap(Path5, PicFloor3);
                        PicNames.Add(PicFloor3 + "1.png");
                        wb.drawMap(Path6, PicFloor4);
                        PicNames.Add(PicFloor4 + "1.png");
                    }
                }

            }
        }
            
        return PicNames;
    }
    void NextStep(int i, int j, int storeID)
    {
        p.X = i;
        p.Y = j;
        int a = -1;
        // lastStep.X = i;
        // lastStep.Y = j;
        OptimalPath.Add(p);
        countSteps++;
        //מכניסים למטריצה -1 בכל מקום שהיינו בו
        switch (storeID / 100)
        {
            case 1:
                map1.floor0[i, j] = a;
                break;
            case 2:
                map1.floor2[i, j] = a;
                break;
            case 3:
                map1.floor4[i, j] = a;
                break;
            case 4:
                map1.floor6[i, j] = a;
                break;
        }

    }
    public List<Point> CalcPath(int StoreID_current, int i_current, int j_current, int StoreID_destination, int i_destination, int j_destination)
    {
        int i = i_current;
        int j = j_current;
        int new_i, new_j, temp_i, temp_j, k;
        int NextStep1 = 1, NextStep2 = 1, NextStep3 = 1, NextStep4 = 1;
        Point p_floor;
        OptimalPath = new List<Point>();
        switch ((StoreID_current / 100))
        {
            case 1: ClearMat(map1.floor0);
                break;
            case 2: ClearMat(map1.floor2);
                break;
            case 3: ClearMat(map1.floor4);
                break;
            case 4: ClearMat(map1.floor6);
                break;
        }

        //אם החנות באותה קומה
        //if ((StoreID_current / 100) == (StoreID_destination / 100))
        //{
            //כל עוד לא הגעתי ליעד
            while (!(((i == i_destination) && ((j + 1 == j_destination) || (j - 1 == j_destination))) || ((j == j_destination) && ((i + 1 == i_destination) || (i - 1 == i_destination)))))
            {
                // בדיקה לאיזה כיוון צריך להתקדם עם ה I
                if (i > i_destination)
                {
                    // צעד הבא לכיוון היעד
                    new_i = i - 1;
                    // צעד הבא במקרה ואי אפשר להתקדם לכיוון היעד (נתקענו)
                    temp_i = i + 1;
                }
                else
                {
                    new_i = i + 1;
                    temp_i = i - 1;
                }
                //בדיקה מה הצעד הבא האפשרי לפי הקומה בה נממצאים
                switch (StoreID_current / 100)
                {
                    case 1:
                        NextStep1 = map1.floor0[new_i, j];
                        NextStep3 = map1.floor0[temp_i, j];
                        break;
                    case 2:
                        NextStep1 = map1.floor2[new_i, j];
                        NextStep3 = map1.floor2[temp_i, j];
                        break;
                    case 3:
                        NextStep1 = map1.floor4[new_i, j];
                        NextStep3 = map1.floor4[temp_i, j];
                        break;
                    case 4:
                        NextStep1 = map1.floor6[new_i, j];
                        NextStep3 = map1.floor6[temp_i, j];
                        break;
                }
                if (j > j_destination)
                {
                    new_j = j - 1;
                    temp_j = j + 1;
                }
                else
                {
                    new_j = j + 1;
                    temp_j = j - 1;
                }

                switch (StoreID_current / 100)
                {
                    case 1:
                        NextStep2 = map1.floor0[i, new_j];
                        NextStep4 = map1.floor0[i, temp_j];
                        break;
                    case 2:
                        NextStep2 = map1.floor2[i, new_j];
                        NextStep4 = map1.floor2[i, temp_j];
                        break;
                    case 3:
                        NextStep2 = map1.floor4[i, new_j];
                        NextStep4 = map1.floor4[i, temp_j];
                        break;
                    case 4:
                        NextStep2 = map1.floor6[i, new_j];
                        NextStep4 = map1.floor6[i, temp_j];
                        break;
                }


                //אם אפשר להתקדם בכיוון הרצוי
                if ((NextStep1 == 2) || (NextStep2 == 2))
                {
                    if ((NextStep1 == 2) && (NextStep2 == 2))
                    {
                        node n = new node(new Point { X = i, Y = j }, countSteps - 1);
                        lastnode = n;
                    }
                    if (NextStep1 == 2)
                    {
                        i = new_i;
                        NextStep(i, j, StoreID_current);
                    }
                    else
                    {
                        j = new_j;
                        NextStep(i, j, StoreID_current);
                    }
                }
                // אם ניתן להתקדם רק בכיוון ההפוך לכיוון הרצוי
                else
                {
                    //אם יש צומת קודמת נחזור אליה
                    if (lastnode.Count != 0)
                    {
                        for (k = OptimalPath.Count; k > lastnode.Count; k--)
                        {
                            //מחיקת אברי המערך מהצומת הנוכחית עד הצומת הקודמת
                            OptimalPath.Remove(OptimalPath.ElementAt(k - 1));
                        }
                        //חוזרים לצומת הקודם
                        if (lastnode.P.X == i)
                        {
                            i = new_i;
                            j = lastnode.P.Y;
                        }
                        else
                        {
                            i = lastnode.P.X;
                            j = new_j;
                        }
                        NextStep(i, j, StoreID_current);
                    }
                    else
                    {
                        if (NextStep3 == 2)
                        {
                            i = temp_i;
                            NextStep(i, j, StoreID_current);
                        }
                        else
                        {
                            j = temp_j;
                            NextStep(i, j, StoreID_current);
                        }
                    }
                }
            }
       // }
        return OptimalPath;
    }
}