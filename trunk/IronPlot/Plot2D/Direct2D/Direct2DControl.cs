﻿// Copyright (c) 2010 Joe Moorhouse

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using SharpDX;
using SharpDX.Direct3D9;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Markup;
using IronPlot.ManagedD3D;
using System.Windows.Threading;

namespace IronPlot
{
    public partial class Direct2DControl : FrameworkElement
    {
        ImageBrush sceneImage;
        Direct2DImage directImage;
        Grid grid;

        public List<DirectPath> Paths
        {
            get { return directImage.paths; }
        }

        public void AddPath(DirectPath path)
        {
            directImage.paths.Add(path);
            path.DirectImage = directImage;
            path.RecreateDisposables();
        }

        public void RemovePath(DirectPath path)
        {
            directImage.paths.Remove(path);
            path.Dispose();
        }

        public Direct2DControl()
        {
            grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Stretch; grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            directImage = new Direct2DImage();
            sceneImage = directImage.ImageBrush;
            grid.Background = sceneImage;
            sceneImage.TileMode = TileMode.None;
            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(Direct2DControl_IsVisibleChanged);
        }

        public void RequestRender()
        {
            directImage.RequestRender();
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            directImage.RenderScene();
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return this.grid;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            this.grid.Measure(availableSize);
            return this.grid.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = base.ArrangeOverride(finalSize);
            double width = size.Width;
            double height = size.Height;
            grid.Arrange(new Rect(0, 0, width, height));
            directImage.SetImageSize((int)width, (int)height, 96);
            return size;
        }

        /// <summary>
        /// Participates in rendering operations that are directed by the layout system.
        /// </summary>
        /// <param name="sizeInfo">
        /// The packaged parameters, which includes old and new sizes, and which
        /// dimension actually changes.
        /// </param>
        //protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        //{
        //    base.OnRenderSizeChanged(sizeInfo);
        //}

        void Direct2DControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            directImage.Visible = (bool)e.NewValue;
            if (directImage.Visible == true)
            {
                foreach (DirectPath path in Paths) path.RecreateDisposables();
                if (Parent is FrameworkElement) (Parent as FrameworkElement).InvalidateMeasure();
            }
            else foreach (DirectPath path in Paths) path.DisposeDisposables();
        }
    }
}

