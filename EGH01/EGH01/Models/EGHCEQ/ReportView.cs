using System;
using System.Xml;
using EGH01DB.Primitives;

namespace EGH01.Models.EGHCEQ
{
    public class ReportView 
    {
        public int      id           { get; set; }    // идентификатор отчета   
        public Report   parent       { get; set; }    // идентификатор родительского отчета
        public string   stage        { get; set; }    // тип отчета: П, Р, С, Т.
        public DateTime date         { get; set; }    // дата формирования отчета 
        public XmlNode  xmlcontetnt  { get; set; }    // отчет 
        public XmlNode  xslhtmlstyle { get; set; }    // xslt-стиль преобразования в html  // в БД?
        public string   comment      { get; set; }    // комментарий  
        public string   line         { get; set; }    // название отчета
        public string   rep          { get; set; }

    }
}