﻿namespace Destroy
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 直线类,用于表达一条直线的形状,目前来说Line仅支持横线和竖线,不支持射线
    /// </summary>
    public class Line
    {
        public Vector2 StartPos;
        public Vector2 EndPos;
        public List<Vector2> PosList;

        private bool isHorizontal = false;

        public Line(Vector2 startPos, Vector2 endPos)
        {
            StartPos = startPos;
            EndPos = endPos;
            PosList = new List<Vector2>();
            if (StartPos.X == EndPos.X)
            {
                isHorizontal = true;
                int x = StartPos.X;
                int ymin = Math.Min(StartPos.Y, EndPos.Y);
                int ymax = Math.Max(StartPos.Y, EndPos.Y);
                for (int i = ymin; i <= ymax; i++)
                {
                    PosList.Add(new Vector2(x, i));
                }
            }
            else if (StartPos.Y == EndPos.Y)
            {
                int y = StartPos.Y;
                int xmin = Math.Min(StartPos.X, EndPos.X);
                int xmax = Math.Max(StartPos.X, EndPos.X);
                for (int i = xmin; i <= xmax; i++)
                {
                    PosList.Add(new Vector2(i, y));
                }
            }
            else
            {
                Debug.Error("不支持非水平或垂直的直线");
            }
        }

        public string GetStr()
        {
            string rawStr;
            if (isHorizontal)
            {
                rawStr = BoxDrawingCharacter.BoxHorizontal.ToString() + BoxDrawingCharacter.BoxHorizontal.ToString();
            }
            else
            {
                rawStr = BoxDrawingCharacter.BoxVertical.ToString();
            }
            return rawStr;
        }

    }

    /// <summary>
    /// 矩形类,用于返回标准矩形的点集.
    /// TODO:需要重写
    /// </summary>
    public class Rectangle
    {
        /// <summary>
        /// 起始点
        /// </summary>
        public Vector2 StartPos;

        /// <summary>
        /// 宽度高度
        /// </summary>
        public int Width, Height;

        /// <summary>
        /// 点列表
        /// </summary>
        public List<Vector2> PosList;

        /// <summary>
        /// 初始化,给定起始坐标,长,宽. 进行点集的初始化操作
        /// </summary>
        public Rectangle(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            PosList = new List<Vector2>();
            AddMesh();
        }

        /// <summary>
        /// 添加Mesh组件
        /// </summary>
        private void AddMesh()
        {
            //添加上下边框的Mesh
            for (int i = 0; i < Width; i++)
            {
                PosList.Add(new Vector2(i, 0));
                PosList.Add(new Vector2(i, Height - 1));
            }
            //添加左右边框的Mesh
            for (int i = 0; i < Height; i++)
            {
                PosList.Add(new Vector2(0, i));
                PosList.Add(new Vector2(Width - 1, i));
            }
            Sort();
        }

        /// <summary>
        /// 添加贴图
        /// </summary>
        public string GetStr()
        {
            //添加边框的贴图
            StringBuilder sb = new StringBuilder();
            sb.Append(BoxDrawingSupply.GetFirstLine(Width));
            for (int i = 0; i < Height - 2; i++)
            {
                sb.Append(' ');
                sb.Append(BoxDrawingSupply.boxVertical);
                sb.Append(BoxDrawingSupply.boxVertical);
                sb.Append(' ');
            }
            sb.Append(BoxDrawingSupply.GetLastLine(Width));
            return sb.ToString();
        }

        /// <summary>
        /// 去重复排序点集合
        /// </summary>
        public void Sort()
        {
            //使用HashSet去重复. 之后排序
            HashSet<Vector2> set = new HashSet<Vector2>();
            foreach (var v in PosList)
            {
                set.Add(v);
            }
            List<Vector2> newList = new List<Vector2>();
            foreach (var v in set)
            {
                newList.Add(v);
            }
            newList.Sort();
            PosList = newList;
        }
    }
}