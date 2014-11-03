using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAPTest.MapProvider.Tian
{
    public class TianHelper
    {
        public static void GetName()
        {
            string url = "http://ogc.tianditu.com/wfssearch.shtml";
            String strQuest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                    + "<wfs:GetFeature maxFeatures=\"100\" service=\"WFS\" version=\"1.0.0\" xsi:schemaLocation=\"http://www.opengis.net/wfs http://schemas.opengis.net/wfs/1.0.0/wfs.xsd\" xmlns:wfs=\"http://www.opengis.net/wfs\" xmlns:gml=\"http://www.opengis.net/gml\" xmlns:ogc=\"http://www.opengis.net/ogc\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> "
                    + "<wfs:Query typeName=\"DOMAIN_POI_NEW\" srsName=\"EPSG:4326\">"
                    + "<ogc:Filter>"
                    + "<ogc:And> "
                    + "<ogc:PropertyIsLike wildCard=\"*\" singleChar=\".\" escape=\"!\"> "
                    + "<ogc:PropertyName>the_geom</ogc:PropertyName>"
                    + "<ogc:Literal>***北京***</ogc:Literal> "
                    + // 请求的时候仅需要替换 超市 这个关键词就好,如果指定城市搜索，搜索关键词为指定城市的名称  加上空格要搜索的关键字就可以 
                    "</ogc:PropertyIsLike>" + " </ogc:And>" + "</ogc:Filter>"
                    + "</wfs:Query>" + "</wfs:GetFeature>";

            var xmlPara =
                "<ogc:Filter>" +
                "<ogc:DWithin>" +
                "<ogc:PropertyName>the_geom</ogc:PropertyName>" +
                "<gml:Point>" +
                "<gml:coordinates>540000,4210000</gml:coordinates>" +
                "</gml:Point>" +
                "<ogc:Distance unit=\"m\">600</ogc:Distance>" +
                "</ogc:DWithin>" +
                "</ogc:Filter>";
            var str = CommonHelper.GetUrl("POST", url, strQuest);
        }

    }
}
