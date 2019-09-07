using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Media;

namespace Tetris
{
    public partial class TetrisApplication : Form
    {
        public TetrisApplication()
        {
            InitializeComponent();
        }

        public class MyPanel : System.Windows.Forms.Panel
        {
            public MyPanel()
            {
                this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            }
        }

        Bitmap bmp = new Bitmap(PANEL_WIDTH, PANEL_HEIGHT);
        Graphics gr;
        Pen pen = new Pen(Color.FromArgb(255, 255, 0, 0), LINE_WIDTH);
        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 255, 0));
        Figure figure;
        List<Figure> figureList;
        List<Coord> coordList_1_Position1, coordList_2_Position1, coordList_3_Position1, coordList_4_Position1, coordList_5_Position1, coordList_6_Position1, coordList_7_Position1;
        List<List<Coord>> coordListListsPosition1;
        Random rnd;
        Cell[,] gamefieldArray;
        Cell[] tempCellArray;
        int randomResult, count = 0, rowCount = 0;
        bool check = true, moveCheck = true, pauseCheck = false, checkPosition = true, checkRow = true;
        const int GAMEFIELD_WIDTH = 10, GAMEFIELD_HEIGHT = 20, CELL_SIZE = 25, PANEL_WIDTH = 251, PANEL_HEIGHT = 501, LINE_WIDTH = 1;
        const int TAKE_VALUE_0 = 0, TAKE_VALUE_1 = 1, START_COORD_Y1 = 0, START_COORD_Y2 = 25, START_COORD_X1 = 75, START_COORD_X2 = 100, START_COORD_X3 = 125, START_COORD_X4 = 150;
        const int MAX_VALUE_Y = 475, MIN_VALUE_Y = 0, MAX_VALUE_X = 225, MIN_VALUE_X = 0, ONE_ROW_SCORE = 100, TWO_ROW_SCORE = 250, THREE_ROW_SCORE = 500, FOUR_ROW_SCORE = 1000;

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(PANEL_WIDTH, PANEL_HEIGHT);
            gamefieldPanel.BackgroundImage = bmp;
            gr = Graphics.FromImage(bmp);

            gamefieldArray = new Cell[GAMEFIELD_HEIGHT, GAMEFIELD_WIDTH];
            for (int i = 0; i < GAMEFIELD_HEIGHT; i++)
            {
                for (int j = 0; j < GAMEFIELD_WIDTH; j++)
                {
                    gamefieldArray[i, j] = new Cell(j * CELL_SIZE, i * CELL_SIZE, TAKE_VALUE_0);
                }
            }

            tempCellArray = new Cell[GAMEFIELD_WIDTH];
            for (int i = 0; i < GAMEFIELD_WIDTH; i++)
            {
                tempCellArray[i] = new Cell(0, 0, TAKE_VALUE_0);
            }

            messageTextBox.Visible = false;
            descriptionLabel1.Visible = true;
            currentScoreLabel.Visible = true;
            currentScoreLabel.Text = "0";
            rowCount = 0;

            figureList = new List<Figure>();
            coordList_1_Position1 = new List<Coord>();
            coordList_2_Position1 = new List<Coord>();
            coordList_3_Position1 = new List<Coord>();
            coordList_4_Position1 = new List<Coord>();
            coordList_5_Position1 = new List<Coord>();
            coordList_6_Position1 = new List<Coord>();
            coordList_7_Position1 = new List<Coord>();

            coordListListsPosition1 = new List<List<Coord>>();

            figure = new Figure(new Coord(START_COORD_X1, START_COORD_Y1), new Coord(START_COORD_X2, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y1), new Coord(START_COORD_X4, START_COORD_Y1), "iFigure");//I - фигура
            figureList.Add(figure);
            coordList_1_Position1.Add(new Coord(START_COORD_X1, START_COORD_Y1));
            coordList_1_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y1));
            coordList_1_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y1));
            coordList_1_Position1.Add(new Coord(START_COORD_X4, START_COORD_Y1));
            coordListListsPosition1.Add(coordList_1_Position1);

            figure = new Figure(new Coord(START_COORD_X2, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y1), new Coord(START_COORD_X2, START_COORD_Y2), new Coord(START_COORD_X3, START_COORD_Y2), "oFigure");//O - фигура
            figureList.Add(figure);
            coordList_2_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y1));
            coordList_2_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y1));
            coordList_2_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y2));
            coordList_2_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y2));
            coordListListsPosition1.Add(coordList_2_Position1);

            figure = new Figure(new Coord(START_COORD_X2, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y1), new Coord(START_COORD_X4, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y2), "tFigure");//T - фигура
            figureList.Add(figure);
            coordList_3_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y1));
            coordList_3_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y1));
            coordList_3_Position1.Add(new Coord(START_COORD_X4, START_COORD_Y1));
            coordList_3_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y2));
            coordListListsPosition1.Add(coordList_3_Position1);

            figure = new Figure(new Coord(START_COORD_X2, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y2), new Coord(START_COORD_X4, START_COORD_Y2), "zFigure");//Z - фигура
            figureList.Add(figure);
            coordList_4_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y1));
            coordList_4_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y1));
            coordList_4_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y2));
            coordList_4_Position1.Add(new Coord(START_COORD_X4, START_COORD_Y2));
            coordListListsPosition1.Add(coordList_4_Position1);

            figure = new Figure(new Coord(START_COORD_X4, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y2), new Coord(START_COORD_X2, START_COORD_Y2), "sFigure");//S - фигура
            figureList.Add(figure);
            coordList_5_Position1.Add(new Coord(START_COORD_X4, START_COORD_Y1));
            coordList_5_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y1));
            coordList_5_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y2));
            coordList_5_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y2));
            coordListListsPosition1.Add(coordList_5_Position1);

            figure = new Figure(new Coord(START_COORD_X2, START_COORD_Y2), new Coord(START_COORD_X2, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y1), new Coord(START_COORD_X4, START_COORD_Y1), "lFigure");//L - фигура
            figureList.Add(figure);
            coordList_6_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y2));
            coordList_6_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y1));
            coordList_6_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y1));
            coordList_6_Position1.Add(new Coord(START_COORD_X4, START_COORD_Y1));
            coordListListsPosition1.Add(coordList_6_Position1);

            figure = new Figure(new Coord(START_COORD_X2, START_COORD_Y1), new Coord(START_COORD_X3, START_COORD_Y1), new Coord(START_COORD_X4, START_COORD_Y1), new Coord(START_COORD_X4, START_COORD_Y2), "jFigure");//J - фигура
            figureList.Add(figure);
            coordList_7_Position1.Add(new Coord(START_COORD_X2, START_COORD_Y1));
            coordList_7_Position1.Add(new Coord(START_COORD_X3, START_COORD_Y1));
            coordList_7_Position1.Add(new Coord(START_COORD_X4, START_COORD_Y1));
            coordList_7_Position1.Add(new Coord(START_COORD_X4, START_COORD_Y2));
            coordListListsPosition1.Add(coordList_7_Position1);

            rnd = new Random();
            randomResult = rnd.Next(0, figureList.Count);
            figureList[randomResult].DrawFigure(gr, pen, CELL_SIZE);
            timerForTetris.Start();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            controlDescription controlDescription = new controlDescription();
            controlDescription.Show();
        }

        private void TimerForTetris_Tick(object sender, EventArgs e) // если интервал между тиками большой (1сек), то в последнем ряду нужно отключать возможность нажатия кнопки
        {
            KeyPreview = true;
            bmp = new Bitmap(PANEL_WIDTH, PANEL_HEIGHT);
            gamefieldPanel.BackgroundImage = bmp;
            gr = Graphics.FromImage(bmp);
            for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
            {
                if (figureList[randomResult].listCoords[i].coordY == MAX_VALUE_Y || gamefieldArray[((figureList[randomResult].listCoords[i].coordY + CELL_SIZE) / CELL_SIZE), (figureList[randomResult].listCoords[i].coordX / CELL_SIZE)].taken == TAKE_VALUE_1)
                {
                    check = false;
                    for (int j = 0; j < figureList[randomResult].listCoords.Count; j++)
                    {
                        gamefieldArray[(figureList[randomResult].listCoords[j].coordY / CELL_SIZE), (figureList[randomResult].listCoords[j].coordX / CELL_SIZE)].taken = TAKE_VALUE_1;
                        if(gamefieldArray[(figureList[randomResult].listCoords[j].coordY / CELL_SIZE), (figureList[randomResult].listCoords[j].coordX / CELL_SIZE)].coordY == MIN_VALUE_Y)
                        {
                            timerForTetris.Stop();
                            messageTextBox.Visible = true;
                            KeyPreview = false;
                        }
                    }
                    for (int k = 0; k < figureList[randomResult].listCoords.Count; k++)
                    {
                        figureList[randomResult].listCoords[k].coordX = coordListListsPosition1[randomResult][k].coordX;
                        figureList[randomResult].listCoords[k].coordY = coordListListsPosition1[randomResult][k].coordY;
                    }
                    rnd = new Random();
                    randomResult = rnd.Next(0, figureList.Count);
                    figureList[randomResult].position = Position.position1;
                    break;
                }
            }
            for (int i = GAMEFIELD_HEIGHT - 1; i >= 1; i--)
            {
                count = 0;
                for (int j = 0; j < GAMEFIELD_WIDTH; j++)
                {
                    if(gamefieldArray[i, j].taken == TAKE_VALUE_1)
                    {
                        count++;
                    }
                }
                if(count == GAMEFIELD_WIDTH)
                {
                    rowCount++;
                    currentScoreLabel.Text = Convert.ToString(rowCount * ONE_ROW_SCORE);
                    checkRow = false;
                    for (int j = 0; j < GAMEFIELD_WIDTH; j++)
                    {
                        gamefieldArray[i, j].taken = TAKE_VALUE_0;
                    }
                }
                if(checkRow == false)
                {
                    for (int j = 0; j < GAMEFIELD_WIDTH; j++)
                    {
                        tempCellArray[j].taken = gamefieldArray[i, j].taken;
                        gamefieldArray[i, j].taken = gamefieldArray[i - 1, j].taken;
                        gamefieldArray[i - 1, j].taken = tempCellArray[j].taken;
                    }
                }
                if(i == 1)
                {
                    checkRow = true;
                }
            }
            if (check == true)
            {
                for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                {
                    figureList[randomResult].listCoords[i].coordY += CELL_SIZE;
                }
            }
            for (int i = 0; i < GAMEFIELD_HEIGHT; i++)
            {
                for (int j = 0; j < GAMEFIELD_WIDTH; j++)
                {
                    if (gamefieldArray[i, j].taken == TAKE_VALUE_1)
                    {
                        gr.DrawRectangle(pen, gamefieldArray[i, j].coordX, gamefieldArray[i, j].coordY, CELL_SIZE, CELL_SIZE);
                        gr.FillRectangle(solidBrush, gamefieldArray[i, j].coordX + LINE_WIDTH, gamefieldArray[i, j].coordY + LINE_WIDTH, CELL_SIZE - LINE_WIDTH, CELL_SIZE - LINE_WIDTH);
                    }
                }
            }
            figureList[randomResult].DrawFigure(gr, pen, CELL_SIZE);
            check = true;
            gamefieldPanel.Refresh();
        }

        private void TetrisApplication_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'A' || e.KeyChar == 'a' || e.KeyChar == 'Ф' || e.KeyChar == 'ф')
            {
                for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                {
                    if (figureList[randomResult].listCoords[i].coordX != MIN_VALUE_X)
                    {
                        if (gamefieldArray[(figureList[randomResult].listCoords[i].coordY / CELL_SIZE), ((figureList[randomResult].listCoords[i].coordX - CELL_SIZE) / CELL_SIZE)].taken == TAKE_VALUE_0)
                        {
                            moveCheck = true;
                        }
                        else
                        {
                            moveCheck = false;
                            break;
                        }
                    }
                    else
                    {
                        if (figureList[randomResult].listCoords[i].coordX == MIN_VALUE_X)
                        {
                            moveCheck = false;
                            break;
                        }
                    }
                }
                if (moveCheck == true)
                {
                    e.Handled = true;
                    for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                    {
                        figureList[randomResult].listCoords[i].coordX -= CELL_SIZE;
                    }
                }
            }
            if (e.KeyChar == 'D' || e.KeyChar == 'd' || e.KeyChar == 'В' || e.KeyChar == 'в')
            {
                for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                {
                    if (figureList[randomResult].listCoords[i].coordX != MAX_VALUE_X)
                    {
                        if (gamefieldArray[(figureList[randomResult].listCoords[i].coordY / CELL_SIZE), ((figureList[randomResult].listCoords[i].coordX + CELL_SIZE) / CELL_SIZE)].taken == TAKE_VALUE_0)
                        {
                            moveCheck = true;
                        }
                        else
                        {
                            moveCheck = false;
                            break;
                        }
                    }                   else
                    {
                        if (figureList[randomResult].listCoords[i].coordX == MAX_VALUE_X)
                        {
                            moveCheck = false;
                            break;
                        }
                    }
                }
                if (moveCheck == true)
                {
                    e.Handled = true;
                    for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                    {
                        figureList[randomResult].listCoords[i].coordX += CELL_SIZE;
                    }
                }
            }
            if (e.KeyChar == 'P' || e.KeyChar == 'p' || e.KeyChar == 'З' || e.KeyChar == 'з')
            {
                if(pauseCheck == false)
                {
                    pauseCheck = true;
                    timerForTetris.Stop();
                    messageTextBox.Visible = true;
                    messageTextBox.Text = "Пауза";
                }
                else
                {
                    pauseCheck = false;
                    timerForTetris.Start();
                    messageTextBox.Visible = false;
                    messageTextBox.Text = "Игра окончена";
                }
            }

            if (e.KeyChar == 'O' || e.KeyChar == 'o' || e.KeyChar == 'Щ' || e.KeyChar == 'щ')
            {
                if(figureList[randomResult].figureName == "iFigure")
                {
                    count = 0;
                    for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                    {
                        if(figureList[randomResult].listCoords[i].coordX < CELL_SIZE || figureList[randomResult].listCoords[i].coordX > MAX_VALUE_X - 2 * CELL_SIZE || figureList[randomResult].listCoords[i].coordY < CELL_SIZE || figureList[randomResult].listCoords[i].coordY > MAX_VALUE_Y - 2 * CELL_SIZE)
                        {
                            count++;
                        }
                    }
                    if(count == 4)
                    {
                        checkPosition = false;
                    }
                    if (checkPosition == true)
                    {
                        e.Handled = true;
                        switch (figureList[randomResult].position)
                        {
                            case Position.position1:
                                figureList[randomResult].position = Position.position2;
                                figureList[randomResult].listCoords[0].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX -= 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY += 2 * CELL_SIZE;
                                break;
                            case Position.position2:
                                figureList[randomResult].position = Position.position1;
                                figureList[randomResult].listCoords[0].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX += 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY -= 2 * CELL_SIZE;
                                break;
                        }
                    }
                    checkPosition = true;
                }
                if (figureList[randomResult].figureName == "tFigure")
                {
                    count = 0;
                    for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                    {
                        if (figureList[randomResult].listCoords[i].coordX < CELL_SIZE || figureList[randomResult].listCoords[i].coordX > MAX_VALUE_X - CELL_SIZE)
                        {
                            count++;
                        }
                    }
                    if (count == 3)
                    {
                        checkPosition = false;
                    }
                    if (checkPosition == true)
                    {
                        e.Handled = true;
                        switch (figureList[randomResult].position)
                        {
                            case Position.position1:
                                figureList[randomResult].position = Position.position2;
                                figureList[randomResult].listCoords[0].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY += 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX += CELL_SIZE;
                                break;
                            case Position.position2:
                                figureList[randomResult].position = Position.position3;
                                figureList[randomResult].listCoords[0].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY += 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX -= CELL_SIZE;
                                break;
                            case Position.position3:
                                figureList[randomResult].position = Position.position4;
                                figureList[randomResult].listCoords[0].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY -= 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX -= CELL_SIZE;
                                break;
                            case Position.position4:
                                figureList[randomResult].position = Position.position1;
                                figureList[randomResult].listCoords[0].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY -= 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX += CELL_SIZE;
                                break;
                        }
                    }
                    checkPosition = true;
                }
                if (figureList[randomResult].figureName == "zFigure")
                {
                    count = 0;
                    for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                    {
                        if (figureList[randomResult].listCoords[i].coordX > MAX_VALUE_X - CELL_SIZE || figureList[randomResult].listCoords[i].coordY < CELL_SIZE)
                        {
                            count++;
                        }
                    }
                    if (count == 2)
                    {
                        checkPosition = false;
                    }
                    if (checkPosition == true)
                    {
                        e.Handled = true;
                        switch (figureList[randomResult].position)
                        {
                            case Position.position1:
                                figureList[randomResult].position = Position.position2;
                                figureList[randomResult].listCoords[0].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY -= 2 * CELL_SIZE;
                                break;
                            case Position.position2:
                                figureList[randomResult].position = Position.position1;
                                figureList[randomResult].listCoords[0].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY += 2 * CELL_SIZE;
                                break;
                        }
                    }
                    checkPosition = true;
                }
                if (figureList[randomResult].figureName == "sFigure")
                {
                    count = 0;
                    for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                    {
                        if (figureList[randomResult].listCoords[i].coordX < CELL_SIZE || figureList[randomResult].listCoords[i].coordY < CELL_SIZE)
                        {
                            count++;
                        }
                    }
                    if (count == 2)
                    {
                        checkPosition = false;
                    }
                    if (checkPosition == true)
                    {
                        e.Handled = true;
                        switch (figureList[randomResult].position)
                        {
                            case Position.position1:
                                figureList[randomResult].position = Position.position2;
                                figureList[randomResult].listCoords[0].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX += 2 * CELL_SIZE;
                                break;
                            case Position.position2:
                                figureList[randomResult].position = Position.position1;
                                figureList[randomResult].listCoords[0].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX -= 2 * CELL_SIZE;
                                break;
                        }
                    }
                    checkPosition = true;
                }
                if (figureList[randomResult].figureName == "lFigure")
                {
                    count = 0;
                    for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                    {
                        if (figureList[randomResult].listCoords[i].coordY < CELL_SIZE || figureList[randomResult].listCoords[i].coordX < CELL_SIZE || figureList[randomResult].listCoords[i].coordY > MAX_VALUE_Y - CELL_SIZE || figureList[randomResult].listCoords[i].coordX > MAX_VALUE_X - CELL_SIZE)
                        {
                            count++;
                        }
                    }
                    if (count == 3)
                    {
                        checkPosition = false;
                    }
                    if (checkPosition == true)
                    {
                        e.Handled = true;
                        switch (figureList[randomResult].position)
                        {
                            case Position.position1:
                                figureList[randomResult].position = Position.position2;
                                figureList[randomResult].listCoords[0].coordX += 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY += 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX -= CELL_SIZE;
                                break;
                            case Position.position2:
                                figureList[randomResult].position = Position.position3;
                                figureList[randomResult].listCoords[0].coordY -= 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY += CELL_SIZE;
                                break;
                            case Position.position3:
                                figureList[randomResult].position = Position.position4;
                                figureList[randomResult].listCoords[0].coordX -= 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY += CELL_SIZE;
                                break;
                            case Position.position4:
                                figureList[randomResult].position = Position.position1;
                                figureList[randomResult].listCoords[0].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY -= 2 * CELL_SIZE;
                                break;
                        }
                    }
                    checkPosition = true;
                }
                if (figureList[randomResult].figureName == "jFigure")
                {
                    count = 0;
                    for (int i = 0; i < figureList[randomResult].listCoords.Count; i++)
                    {
                        if (figureList[randomResult].listCoords[i].coordY < CELL_SIZE || figureList[randomResult].listCoords[i].coordX < CELL_SIZE || figureList[randomResult].listCoords[i].coordY > MAX_VALUE_Y - CELL_SIZE || figureList[randomResult].listCoords[i].coordX > MAX_VALUE_X - CELL_SIZE)
                        {
                            count++;
                        }
                    }
                    if (count == 3)
                    {
                        checkPosition = false;
                    }
                    if (checkPosition == true)
                    {
                        e.Handled = true;
                        switch (figureList[randomResult].position)
                        {
                            case Position.position1:
                                figureList[randomResult].position = Position.position2;
                                figureList[randomResult].listCoords[0].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY += 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY -= CELL_SIZE;
                                break;
                            case Position.position2:
                                figureList[randomResult].position = Position.position3;
                                figureList[randomResult].listCoords[0].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX -= 2 * CELL_SIZE;
                                break;
                            case Position.position3:
                                figureList[randomResult].position = Position.position4;
                                figureList[randomResult].listCoords[0].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[0].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY += CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY += 2 * CELL_SIZE;
                                break;
                            case Position.position4:
                                figureList[randomResult].position = Position.position1;
                                figureList[randomResult].listCoords[0].coordX -= CELL_SIZE;
                                figureList[randomResult].listCoords[1].coordY -= CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordX += CELL_SIZE;
                                figureList[randomResult].listCoords[2].coordY -= 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordX += 2 * CELL_SIZE;
                                figureList[randomResult].listCoords[3].coordY -= CELL_SIZE;
                                break;
                        }
                    }
                    checkPosition = true;
                }
            }

            if (e.KeyChar == 'S' || e.KeyChar == 's' || e.KeyChar == 'Ы' || e.KeyChar == 'ы') // после нажатия происходит "падение" фигурки в максимально нижнее незанятое место в "ширине", в которой была нажата клавиша
            {
                e.Handled = true;
            }
        }

        class Figure
        {
            public List<Coord> listCoords;
            public string figureName;
            public Position position;

            public Figure(Coord coord1, Coord coord2, Coord coord3, Coord coord4, string name)
            {
                listCoords = new List<Coord>();
                listCoords.Add(coord1);
                listCoords.Add(coord2);
                listCoords.Add(coord3);
                listCoords.Add(coord4);
                figureName = name;
                position = Position.position1;
            }

            public void DrawFigure(Graphics gr, Pen pen, int cellSize)
            {
                for (int i = 0; i < listCoords.Count; i++)
                {
                    gr.DrawRectangle(pen, listCoords[i].coordX, listCoords[i].coordY, cellSize, cellSize);
                    SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 255, 0));
                    gr.FillRectangle(solidBrush, listCoords[i].coordX + LINE_WIDTH, listCoords[i].coordY + LINE_WIDTH, cellSize - LINE_WIDTH, cellSize - LINE_WIDTH);
                }
            }
        }

        class Coord
        {
            public int coordX;
            public int coordY;

            public Coord(int x, int y)
            {
                coordX = x;
                coordY = y;
            }
        }

        class Cell
        {
            public int coordX;
            public int coordY;
            public int taken;

            public Cell(int x, int y, int z)
            {
                coordX = x;
                coordY = y;
                taken = z;
            }
        }

        enum Position
        { position1, position2, position3, position4 }
    }
}
