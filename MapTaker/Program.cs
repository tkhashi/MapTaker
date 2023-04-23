using System.Data;
using System.Text;
using NetTopologySuite;
using SharpMap;
using SharpMap.Data.Providers;

const string sBase = @"C:\Users\k_tak\Downloads\県送付データ（水戸市）\図形";

//文字コード変換用
var enc = Encoding.GetEncoding(Encoding.UTF8.CodePage);

//これが無いとエラーになる
var services = new NtsGeometryServices();
var mp = new Map();

//ポイントデータ
var sShp = Path.Combine(sBase, "人孔.shp"); //シェープファイル
//string sCsv = Path.Combine(sBase, "point.csv"); //情報を格納するcsvファイル
//using (StreamWriter sw = new StreamWriter(sCsv, false, enc))
using var pointShapeFile = new ShapeFile(sShp);
//sw.WriteLine("x,y,Name");
pointShapeFile.Encoding = enc; //これを設定しないと文字化けする
for (uint i = 0; i < pointShapeFile.GetFeatureCount(); i++)
{
    //SharpMap.Data.FeatureDataRow
    //System.Data.DataRowに座標情報とかが付いたような感じ
    var fdr = pointShapeFile.GetFeature(i);
    var a = fdr.GetColumnsInError();
    var sLine = fdr.Geometry.Coordinate.X.ToString("0.0")
                + "," + fdr.Geometry.Coordinate.Y.ToString("0.0");
    //+ "," + fdr["Name"];
    //sw.WriteLine(sLine);
    Console.WriteLine(sLine);
}

//ラインデータ
sShp = Path.Combine(sBase, "管渠.shp");
//sCsv = Path.Combine(sBase, "line.csv");
//using (StreamWriter sw = new StreamWriter(sCsv, false, enc))
using var lineShapeFile = new ShapeFile(sShp);
//sw.WriteLine("x,y,Name");
lineShapeFile.Encoding = enc;
for (uint i = 0; i < lineShapeFile.GetFeatureCount(); i++)
{
    var fdr = lineShapeFile.GetFeature(i);
    DataRow dr = fdr;
    foreach (var coordinate in fdr.Geometry.Coordinates)
    {
        var sLine = coordinate.X.ToString("0.0")
                    + "," + coordinate.Y.ToString("0.0");
        //+ "," + fdr["Name"];
        //sw.WriteLine(sLine);
        Console.WriteLine(sLine);
    }
}

////ポリゴンデータ
//sShp = Path.Combine(sBase, "polygon.shp");
////sCsv = Path.Combine(sBase, "polygon.csv");
////using (StreamWriter sw = new StreamWriter(sCsv, false, enc))
//using (ShapeFile sf = new ShapeFile(sShp))
//{
//  //sw.WriteLine("x,y,Name");
//  sf.Encoding = enc;
//  for (uint i = 0; i < sf.GetFeatureCount(); i++)
//  {
//    FeatureDataRow fdr = sf.GetFeature(i);
//    for (int j = 0; j < fdr.Geometry.Coordinates.Length; j++)
//    {
//      string sLine = fdr.Geometry.Coordinates[j].X.ToString("0.0")
//        + "," + fdr.Geometry.Coordinates[j].Y.ToString("0.0")
//        + "," + fdr["Name"];
//      //sw.WriteLine(sLine);
//    }
//  }
//}