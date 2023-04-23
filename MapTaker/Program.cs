using System.Text;
using System.Text.Unicode;
using GeoAPI;
using SharpMap;
using SharpMap.Layers;
using SharpMap.Data;
using SharpMap.Data.Providers;
using GeoAPI.Geometries;
using NetTopologySuite;

//中略
//ここから本番処理
 
//定数
const string sBase = @"C:\Users\k_tak\Downloads\県送付データ（水戸市）\図形";
 
//文字コード変換用
Encoding enc = Encoding.GetEncoding(Encoding.UTF8.CodePage);

IGeometryServices services = new NtsGeometryServices();
//地図
Map mp = new Map(); //これが無いとエラーになる
 
//ポイントデータ
string sShp = Path.Combine(sBase, "人孔.shp"); //シェープファイル
//string sCsv = Path.Combine(sBase, "point.csv"); //情報を格納するcsvファイル
//using (StreamWriter sw = new StreamWriter(sCsv, false, enc))
using (ShapeFile sf = new ShapeFile(sShp)) //SharpMap.Data.Providers.ShapeFile
{
  //sw.WriteLine("x,y,Name");
  sf.Encoding = enc; //これを設定しないと文字化けする
  for (uint i = 0; i < sf.GetFeatureCount(); i++)
  {
    //SharpMap.Data.FeatureDataRow
    //System.Data.DataRowに座標情報とかが付いたような感じ
    FeatureDataRow fdr = sf.GetFeature(i);
    var a = fdr.GetColumnsInError();
    string sLine = fdr.Geometry.Coordinate.X.ToString("0.0")
                   + "," + fdr.Geometry.Coordinate.Y.ToString("0.0");
      //+ "," + fdr["Name"];
    //sw.WriteLine(sLine);
    Console.WriteLine(sLine);
  }
}
 
//ラインデータ
sShp = Path.Combine(sBase, "管渠.shp");
//sCsv = Path.Combine(sBase, "line.csv");
//using (StreamWriter sw = new StreamWriter(sCsv, false, enc))
using (ShapeFile sf = new ShapeFile(sShp))
{
  //sw.WriteLine("x,y,Name");
  sf.Encoding = enc;
  for (uint i = 0; i < sf.GetFeatureCount(); i++)
  {
    FeatureDataRow fdr = sf.GetFeature(i);
    System.Data.DataRow dr = fdr;
    for (int j = 0; j < fdr.Geometry.Coordinates.Length; j++)
    {
        string sLine = fdr.Geometry.Coordinates[j].X.ToString("0.0")
                       + "," + fdr.Geometry.Coordinates[j].Y.ToString("0.0");
        //+ "," + fdr["Name"];
        //sw.WriteLine(sLine);
        Console.WriteLine(sLine);
    }
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
