using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Contexts;
using System.Drawing.Text;

//using MEM_AlbañileriaLines.Properties;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MEM_AlbañileriaLines
{
   

    public partial class VentanaInicial : System.Windows.Forms.Form
    {
        UIApplication uiapp;
        Document doc;
        ICollection<TipoMuro> tipos = new List<TipoMuro>();


        public VentanaInicial( UIApplication ui)
        {
            uiapp = ui;
            doc = uiapp.ActiveUIDocument.Document;
            InitializeComponent();
        }
       
        private void VentanaInicial_Load(object sender, EventArgs e)
        {
            


            //Añado los datos del ComboBox De Materiales
            List<string> list = new List<string>();
            list.Add("Comentario"); list.Add("Descripción"); list.Add("Nota Clave"); list.Add("Marca");
            foreach (string p in list)
            {
                comboBoxMaterialParam.Items.Add(p);
                comboBoxMaterialDescripción.Items.Add(p);
            }
            comboBoxMaterialParam.SelectedItem = comboBoxMaterialParam.Items[0];
            comboBoxMaterialDescripción.SelectedItem = comboBoxMaterialDescripción.Items[1];


            //Busco los tipos de muros unicos
            ICollection<Element> Ewalls = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements();

            ICollection<Element> walls = new List<Element>();
            List<string> ides = new List<string>();
            foreach (Element Ew in Ewalls)
            {
                Element tipoAc = doc.GetElement(Ew.GetTypeId());
                if (ides.Contains(tipoAc.Id.ToString()) == false)
                {
                   
                    ides.Add(tipoAc.Id.ToString());
                    walls.Add(tipoAc);
                    

                }

            }

            //CREO TODOS LOS TIPOS DE MUROS Y SUS MATERIALES
            foreach (Element ele in walls)
            {
                TipoMuro tipoM = new TipoMuro();
                //MUROS BÁSICOS

                string tipodemuro = ele.get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME).AsString();
                tipoM.familyName = tipodemuro;

                #region basicWall
                if (tipodemuro == "Basic Wall" || tipodemuro == "Muro básico")
                {
                    WallType wall = ele as WallType;
                    //textBox1.Text = textBox1.Text + ele.Name.ToString();

                    tipoM.nombreTipo = wall.Name;
                    tipoM.funcion = wall.get_Parameter(BuiltInParameter.FUNCTION_PARAM).AsValueString();
                    tipoM.anchuraTipo = wall.get_Parameter(BuiltInParameter.WALL_ATTR_WIDTH_PARAM).AsDouble();
                    tipoM.idTipo = wall.Id;





                    //ANTER DE ARRANCAR LA APLICACIÓN ALMACENO EL VALOR DE KEYNOTE YA QUE AÚN NO NO HA PODIDO SELECCIONAR NINGUNO EL USUARIO
                    tipoM.notaClave = wall.get_Parameter(BuiltInParameter.KEYNOTE_PARAM).AsString();

                    if (tipoM.notaClave == null)
                    {
                        tipoM.notaClave = "No existe nota clave";
                    }

                    try
                    {
                        tipoM.fueraDeLeyenda = wall.get_Parameter(BuiltInParameter.ALL_MODEL_MANUFACTURER).AsString();
                    }
                    catch { }


                    try
                    {
                        Parameter comentariosdeTipo = wall.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_COMMENTS);

                        tipoM.comentariosTipo = comentariosdeTipo.AsString();
                    }
                    catch { }


                    #region Materiales
                    ICollection<material> mats = new List<material>() { };
                    try
                    {
                        IList<CompoundStructureLayer> layers = wall.GetCompoundStructure().GetLayers();

                        BuiltInParameter builtCode = BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS;
                        BuiltInParameter builtDesc = BuiltInParameter.ALL_MODEL_DESCRIPTION;

                        string selcodes = comboBoxMaterialParam.SelectedItem.ToString();

                        string seldescription = comboBoxMaterialDescripción.SelectedItem.ToString();

                        if (selcodes == "Nota Clave") { builtCode = BuiltInParameter.KEYNOTE_PARAM; }
                        if (selcodes == "Marca") { builtCode = BuiltInParameter.ALL_MODEL_MARK; }
                        if (selcodes == "Descripción") { builtCode = BuiltInParameter.ALL_MODEL_DESCRIPTION; }

                        if (seldescription == "Nota Clave") { builtDesc = BuiltInParameter.KEYNOTE_PARAM; }
                        if (seldescription == "Marca") { builtDesc = BuiltInParameter.ALL_MODEL_MARK; }
                        if (seldescription == "Descripción") { builtDesc = BuiltInParameter.ALL_MODEL_DESCRIPTION; }

                        foreach (CompoundStructureLayer Clayer in layers)
                        {
                            try
                            {
                                Material material = doc.GetElement(Clayer.MaterialId) as Material;
                                //textBox1.Text = textBox1.Text + " - Mat: " + material.Name.ToString();
                                material mat = new material();
                                mat.anchuraCapa = Clayer.Width;
                                mat.codigo = material.get_Parameter(builtCode).AsString();
                                mat.descripcion = material.get_Parameter(builtDesc).AsString();
                                mat.nombre = material.Name;

                                Autodesk.Revit.DB.Color color = material.Color;
                                mat.R = color.Red;
                                mat.G = color.Green;
                                mat.B = color.Blue;

                                //SI ALGUN CODIGO ESTÁ EN BLANCO NO SE PUEDE RENOMBRAR
                                if (mat.codigo == "")
                                {
                                    tipoM.editable = false;
                                    tipoM.errormessage = "Comentario de material no existe: " + mat.nombre;
                                }

                                //ahora añado el material a la lista
                                mats.Add(mat);
                            }
                            catch
                            {
                                tipoM.editable = false;
                                tipoM.errormessage = "Material no asignado correctamente";

                            }

                        }
                        tipoM.materialesMuro = mats;
                        tipoM.nombreModif = tipoM.addNombreBasicW(tipoM.materialesMuro);
                        if (tipoM.fueraDeLeyenda != "0")
                        {
                            tipos.Add(tipoM);
                        }

                    }
                    catch
                    {

                    }
                    #endregion
                }
                #endregion


                #region Curtain wall
                if (tipodemuro == "Curtain Wall" || tipodemuro == "Muro cortina")
                {
                    WallType wall = ele as WallType;
                    //textBox1.Text = textBox1.Text + ele.Name.ToString();

                    tipoM.nombreTipo = wall.Name;
                    tipoM.funcion = wall.get_Parameter(BuiltInParameter.FUNCTION_PARAM).AsValueString();
                    tipoM.JustificationHori = wall.get_Parameter(BuiltInParameter.SPACING_LAYOUT_HORIZ).AsValueString();
                    tipoM.JustificationVert = wall.get_Parameter(BuiltInParameter.SPACING_LAYOUT_VERT).AsValueString();
                    tipoM.spacingHori = wall.get_Parameter(BuiltInParameter.SPACING_LENGTH_HORIZ).AsDouble() / 0.0328084;
                    tipoM.spacingVert = wall.get_Parameter(BuiltInParameter.SPACING_LENGTH_VERT).AsDouble() / 0.0328084;

                    tipoM.panelTypeId = wall.get_Parameter(BuiltInParameter.AUTO_PANEL_WALL).AsElementId();



                    tipoM.idTipo = wall.Id;



                    try
                    {
                        tipoM.panelTypeComment = doc.GetElement(tipoM.panelTypeId).get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_COMMENTS).AsString();
                        tipoM.editable = true;
                    }

                    catch { tipoM.editable = false; tipoM.errormessage = tipoM.errormessage + "No hay comentario de panel"; }

                    try
                    {
                        tipoM.editable = true;
                        tipoM.nombreModif = tipoM.addNombreCurtainW();

                    }
                    catch { tipoM.editable = false; tipoM.errormessage = tipoM.errormessage + "No hay comentario de panel"; }
                    tipos.Add(tipoM);
                }
                #endregion

            }

        }

    




        public ICollection<string> getParameterValueByName(ICollection<TipoMuro> listOri,string paramName)
        {
            ICollection<string> strings = new List<string>();
            foreach (TipoMuro tm2 in listOri)
            {
                Parameter current = doc.GetElement(tm2.idTipo).LookupParameter(paramName);
                if (current.AsString() != "")
                {
                    strings.Add(current.AsString());
                }
                if(current.AsValueString()!="")
                {
                    strings.Add(current.AsValueString());
                }
                
            }

            return strings;
        }

        public double convertUnits(double d, Autodesk.Revit.DB.View v)
        {

            double shift = UnitUtils.ConvertToInternalUnits(d, UnitTypeId.Meters); //v.Scale para modificar el valor según la escala (no lo entiendo aún)
            //Autodesk.Revit.DB.DisplayUnitType.DUT_MILLIMETERS para versiones 21 hacia atrás
            return shift;
        }

        private void buttonCrearRegiones_Click(object sender, EventArgs e)
        {
            Category c = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);
            CategoryNameMap subcats = c.SubCategories;

            //limpio la imagen del picturebox
            double distLines = 0;

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(System.Drawing.Color.White);

            //Guardo las lineas para colocarlas lo último, de esa fortma salen delante
            List<(System.Drawing.Point, System.Drawing.Point)> lineas = new List<(System.Drawing.Point, System.Drawing.Point)>();

            System.Drawing.Color col = System.Drawing.Color.ForestGreen;//Color inicial
            foreach (Category lineStyle in subcats)
            {

                try
                {
                    col = System.Drawing.Color.FromArgb(0, lineStyle.LineColor.Red, lineStyle.LineColor.Green, lineStyle.LineColor.Blue);

                    Element linepattern = doc.GetElement(lineStyle.GetLinePatternId(GraphicsStyleType.Projection));



                }
                catch
                {

                }


                #region dibujar lineas en picturebox

                try
                {


                    //Fuente
                    Font fnt = new Font("Arial", 8);

                    distLines = distLines + 10;




                    //g.DrawString(matActual.nombre + " _ " + Math.Round(distLines, 2) + "cm", fnt, System.Drawing.Brushes.Black, new System.Drawing.Point(inicioTextos + 5, alturaTextoMat - 8));
                    lineas.Add((new System.Drawing.Point(0, Convert.ToInt32(distLines)), new System.Drawing.Point(40, Convert.ToInt32(distLines))));
                    //g.DrawLine(Pens.Black, new System.Drawing.Point(posicionMaterialY + Convert.ToInt32(anchuraMaterial) / 2, alturaTextoMat), new System.Drawing.Point(inicioTextos, alturaTextoMat));



                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message.ToString());
                }

                #endregion





            }
            foreach ((System.Drawing.Point, System.Drawing.Point) p in lineas)
            {
                System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.AliceBlue);
                g.DrawLine(pen, p.Item1, p.Item2);
            }

            //g.Clear(System.Drawing.Color.Firebrick);
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {

        }

        private void label14_MouseHover(object sender, EventArgs e)
        {

        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {

        }

        private void label13_MouseHover(object sender, EventArgs e)
        {

        }

        private void textBoxSelector_TextChanged(object sender, EventArgs e)
        {
            string actual = textBoxSelector.Text;
            if (actual == "")
            {
                dataGridView1.Rows.Clear();

                foreach (TipoMuro t in tipos)
                {
                    dataGridView1.Rows.Add(true, t.nombreTipo);
                }
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (TipoMuro t in tipos)
                {
                    if (t.nombreTipo.Contains(actual))
                    {
                        dataGridView1.Rows.Add(true, t.nombreTipo);
                    }
                }
            }
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string tipeNameSelected = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TipoMuro tipoSelec = new TipoMuro();

            foreach (TipoMuro m in tipos)
            {
                if (m.nombreTipo == tipeNameSelected) { tipoSelec = m; }
            }

            updatePicture01(tipoSelec);
            updatePicture02(tipoSelec);
            
        }

        
        private void updatePicture01(TipoMuro t)
        {
            try
            {
                ICollection<material> wallMaterials = t.materialesMuro;
                int Altura = 200;
                int inicioTextos = 80;
                int alturaTextoMat = 35;
                int posicionMaterialY = 30;
                int posicionMaterialX = 30;
                Graphics g = pictureBox1.CreateGraphics();
                g.Clear(System.Drawing.Color.White);

                //Guardo las lineas para colocarlas lo último, de esa fortma salen delante
                List<(System.Drawing.Point, System.Drawing.Point)> lineas = new List<(System.Drawing.Point, System.Drawing.Point)>();
                foreach (material matActual in wallMaterials)
                {
                    try
                    {
                        // Create a local version of the graphics object for the PictureBox.

                        //Fuente
                        Font fnt = new Font("Arial", 8);

                        double anchuraMaterial = (matActual.anchuraCapa) / 0.032808;

                        System.Drawing.Color customscolor = System.Drawing.Color.FromArgb(matActual.R, matActual.G, matActual.B);
                        SolidBrush brush = new SolidBrush(customscolor);
                        RectangleF rec = new RectangleF(posicionMaterialY, posicionMaterialX, Convert.ToInt32(anchuraMaterial), Altura);
                        System.Drawing.Rectangle rec1 = new System.Drawing.Rectangle(posicionMaterialY, posicionMaterialX, Convert.ToInt32(anchuraMaterial), Altura);
                        g.FillRectangle(brush, rec);
                        g.DrawRectangle(Pens.Black, rec1);
                        g.DrawString(matActual.nombre + " _ " + Math.Round(anchuraMaterial, 2) + "cm", fnt, System.Drawing.Brushes.Black, new System.Drawing.Point(inicioTextos + 5, alturaTextoMat - 8));
                        lineas.Add((new System.Drawing.Point(posicionMaterialY + Convert.ToInt32(anchuraMaterial) / 2, alturaTextoMat), new System.Drawing.Point(inicioTextos, alturaTextoMat)));
                        //g.DrawLine(Pens.Black, new System.Drawing.Point(posicionMaterialY + Convert.ToInt32(anchuraMaterial) / 2, alturaTextoMat), new System.Drawing.Point(inicioTextos, alturaTextoMat));

                        alturaTextoMat += 20;
                        posicionMaterialY += Convert.ToInt32(anchuraMaterial);
                    }
                    catch
                    {

                    }


                }
                foreach ((System.Drawing.Point, System.Drawing.Point) p in lineas) { g.DrawLine(Pens.Black, p.Item1, p.Item2); }
            }
            catch { }
        }
        public void updatePicture02(TipoMuro t)
        {

            int anchuraImagen = pictureBox2.Width;
            try
            {
                ICollection<material> wallMaterials = t.materialesMuro;
                int Altura = 200;
                int inicioTextos = 80;
                int alturaTextoMat = 35;
                int posicionMaterialY = (anchuraImagen / 2) - Convert.ToInt32(t.anchuraTipo)-30;
                int posicionMaterialX = 30;
                Graphics g = pictureBox2.CreateGraphics();
                g.Clear(System.Drawing.Color.White);

                //Guardo las lineas para colocarlas lo último, de esa fortma salen delante
                List<(System.Drawing.Point, System.Drawing.Point)> lineas = new List<(System.Drawing.Point, System.Drawing.Point)>();
                foreach (material matActual in wallMaterials)
                {
                    try
                    {
                        // Create a local version of the graphics object for the PictureBox.

                        //Fuente
                        Font fnt = new Font("Arial", 8);

                        double anchuraMaterial = (matActual.anchuraCapa) / 0.032808;

                        System.Drawing.Color customscolor = System.Drawing.Color.FromArgb(matActual.R, matActual.G, matActual.B);
                        SolidBrush brush = new SolidBrush(customscolor);
                        RectangleF rec = new RectangleF(posicionMaterialY, posicionMaterialX, Convert.ToInt32(anchuraMaterial), Altura);
                        System.Drawing.Rectangle rec1 = new System.Drawing.Rectangle(posicionMaterialY, posicionMaterialX, Convert.ToInt32(anchuraMaterial), Altura);
                        g.FillRectangle(brush, rec);
                        g.DrawRectangle(Pens.Black, rec1);
                        //g.DrawString(matActual.nombre + " _ " + Math.Round(anchuraMaterial, 2) + "cm", fnt, System.Drawing.Brushes.Black, new System.Drawing.Point(inicioTextos + 5, alturaTextoMat - 8));
                        //lineas.Add((new System.Drawing.Point(posicionMaterialY + Convert.ToInt32(anchuraMaterial) / 2, alturaTextoMat), new System.Drawing.Point(inicioTextos, alturaTextoMat)));
                        //g.DrawLine(Pens.Black, new System.Drawing.Point(posicionMaterialY + Convert.ToInt32(anchuraMaterial) / 2, alturaTextoMat), new System.Drawing.Point(inicioTextos, alturaTextoMat));

                        //alturaTextoMat += 20;
                        posicionMaterialY += Convert.ToInt32(anchuraMaterial);
                    }
                    catch
                    {

                    }


                }
                //foreach ((System.Drawing.Point, System.Drawing.Point) p in lineas) { g.DrawLine(Pens.Black, p.Item1, p.Item2); }
            }
            catch { }
        }
    }
  
    public class TipoMuro
    {
        public ICollection<material> materialesMuro { get; set; } //lista de materiales
        public double anchuraTipo { get; set; }//Anchura total del muro
        public string funcion { get; set; }//función del muro

        public string nombreTipo { get; set; }//Nombre del tipo actual sin modificar

        public bool editable = true;//el muro tienen un nombre generable o no?
        public string errormessage { get; set; }//mensaje de error generado desde ejecución

        public string nombreModif { get; set; }//Nuevo nombre creado

        public string comentariosTipo { get; set; }

        public ElementId idTipo { get; set; }
        public string familyName { get; set; }
        public string notaClave { get; set; }
        public string fueraDeLeyenda { get; set; }
        public string títuloSeleccionado { get; set; }

        //Muros cortina valores extra

        public double spacingVert { get; set; }//cm
        public double spacingHori { get; set; }//cm
        public string JustificationVert { get; set; }
        public string JustificationHori { get; set; }
        public string panelName { get; set; }
        public ElementId panelTypeId { get; set; }
        public string panelTypeComment { get; set; }
        

      


        public string addNombreBasicW(ICollection<material> mats)
        {
            string finalName = "NombreCalculado";
            finalName = this.funcion.ToString().Substring(0, 3).ToUpper();



            foreach (material mat in mats)
            {
                finalName = finalName + "_" + mat.codigo;
            }


            string comentarios = "";
            try { comentarios = "_" + this.comentariosTipo.ToString(); } catch { }
            finalName = finalName + "_" + Math.Round(this.anchuraTipo, 2).ToString() + "cm" + comentarios;

            return finalName;
        }
        public string addNombreCurtainW()
        {
            string finalName = "NombreCalculado";
            finalName = this.funcion.ToString().Substring(0, 3).ToUpper();

            string justV = ""; string justH = "";
            //Distancia fija, Número fijo, Espaciado máximo, Espaciado mínimo
            //Fixed Distance, Fixed Number, Maximum Spacing, Minimum Spacing
            if (JustificationVert == "Ninguno" || JustificationVert == "None") justV = "XXX";
            if (JustificationVert == "Distancia fija" || JustificationVert == "Fixed Distance") justV = "DFI";
            if (JustificationVert == "Número fijo" || JustificationVert == "Fixed Number") justV = "NFI";
            if (JustificationVert == "Espaciado máximo" || JustificationVert == "Maximum Spacing") justV = "EMA";
            if (JustificationVert == "Espaciado mínimo" || JustificationVert == "Minimum Spacing") justV = "EMI";

            if (JustificationVert == "Ninguno" || JustificationVert == "None") justH = "XXX";
            if (JustificationHori == "Distancia fija" || JustificationHori == "Fixed Distance") justH = "DFI";
            if (JustificationHori == "Número fijo" || JustificationHori == "Fixed Number") justH = "NFI";
            if (JustificationHori == "Espaciado máximo" || JustificationHori == "Maximum Spacing") justH = "EMA";
            if (JustificationHori == "Espaciado mínimo" || JustificationHori == "Minimum Spacing") justH = "EMI";



            finalName = finalName + "_" + justV + "_" + Math.Round(this.spacingVert, 2).ToString()
                + "_" + justH + "_" + Math.Round(this.spacingHori, 2).ToString();

            try
            {
                string comentT = this.panelTypeComment;
                if (comentT != null) { finalName = finalName + "_Panel_" + comentT; }
            }
            catch { }


            try
            {
                string commentT = this.comentariosTipo.ToString();
                if (commentT != null) { finalName = finalName + "_" + commentT; }
            }
            catch { }

            return finalName;
        }
    }
    public class material
    {
        public string nombre { get; set; }
        public string codigo { get; set; }
        public double anchuraCapa { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public string descripcion { get; set; }

    }
  

}
