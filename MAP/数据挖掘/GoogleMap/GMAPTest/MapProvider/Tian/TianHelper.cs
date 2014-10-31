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
            //var xmlPara = "<?xml version='1.0' encoding='UTF-8'?>"
            //+ "<wfs:GetFeature maxFeatures='100' service='WFS' version='1.1.0' "
            //+ "xmlns:wfs='http://www.opengis.net/wfs' "
            //+ "xmlns:gml='http://www.opengis.net/gml' "
            //+ "xmlns:ogc='http://www.opengis.net/ogc' "
            //+ "xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' "
            //+ "xsi:schemaLocation='http://www.opengis.net/wfs http://schemas.opengis.net/wfs/1.0.0/wfs.xsd'>"
            //+ "<wfs:Query typeName='iso19112:SI_Gazetteer' srsName='EPSG:4326'>"
            //+ "<ogc:Filter xmlns:ogc='http://www.opengis.net/ogc'>"
            //+ "<ogc:And><ogc:PropertyIsLike wildCard='*' singleChar='.' escape='!'>"
            //+ "<ogc:PropertyName>STANDARDNAME</ogc:PropertyName>"
            //+ "<ogc:Literal>beijing</ogc:Literal>"
            //+ "</ogc:PropertyIsLike></ogc:And></ogc:Filter>"
            //+ "</wfs:Query></wfs:GetFeature>";
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
            var str = CommonHelper.GetUrl("POST", url, xmlPara);
        }

    }
}
