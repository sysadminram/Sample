using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SketchUpNET;

namespace SketchUpTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExcecute_Click(object sender, EventArgs e)
        {
             string filePath = txtBoxPath.Text;
            if (!File.Exists(filePath))
                return;
            ExcecuteSketchUp(filePath);
        }

        private static void ExcecuteSketchUp(string filePath)
        {
            try
            {
                SketchUp skp = new SketchUp();
                if (skp.LoadModel(filePath))
                {
                    List<Group> lstGroups = skp.Groups;
                    Dictionary<string, SketchUpNET.Component> dictComponentInfo = skp.Components;
                    List<Instance> lstInstances = skp.Instances;
                    List<Surface> lstSurfaces = skp.Surfaces;

                    for (int i = 0; i < lstSurfaces.Count; i++)
                    {
                        Surface surf = lstSurfaces[i];
                        double area = surf.Area;
                        Vector normal = surf.Normal;
                        List<Loop> lsInnerLoops = surf.InnerEdges;
                        Loop outerLoops = surf.OuterEdges;
                        List<Edge> lstEdges = outerLoops.Edges;

                        List<Vertex> lstVertices = surf.Vertices;
                    }

                    //foreach (Surface srf in skp.Surfaces)
                    //    surfaces.Add(new GH_Brep(srf.ToRhinoGeo()));

                    //foreach (Layer l in skp.Layers)
                    //    layers.Add(new GH_String(l.Name));

                    //foreach (Instance i in skp.Instances)
                    //    Instances.Add(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                 txtBoxPath.Text = dialog.FileName;
            }
        }
    }
}
