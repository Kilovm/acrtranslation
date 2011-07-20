using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                if (args.Length < 2)
                {
                    Console.WriteLine("format: BMGStatistics textDir dest");
                    return;
                }

                Regex regex = new Regex(@"(\[\d+\])|\s");

                DirectoryInfo root = new DirectoryInfo(args[0]);

                using (StreamWriter writer = new StreamWriter(args[1]))
                {
                    writer.WriteLine("<html>");
                    writer.WriteLine(string.Format("<head><title>{0}</title></head>", root.Name));

                    writer.WriteLine("<body>");
                    writer.WriteLine("<table style='border-style:solid;border-width:1px;width:80%;border-color:black;'>");

                    writer.WriteLine(@"<tr>
                                    <th>&nbsp;</th>
                                    <th>File</th>
                                    <th>Lines</th>
                                    <th>Non-Empty</th>
                                    <th>Translated</th>
                                    <th>Percent</th>
                                    </tr>");

                    bool f = false;
                    int n = 1;
                    int sensTotal = 0;
                    int transTotal = 0;
                    int orgTotal = 0;

                    FileInfo[] files = root.GetFiles("*.xml", SearchOption.AllDirectories);

                    foreach (FileInfo info in files)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(info.FullName);

                        if (!doc.DocumentElement.Name.Equals("Session")) continue;

                        XmlNodeList nlist = doc.DocumentElement.GetElementsByTagName("Sentence");

                        int transCount = 0;
                        int orgCount = 0;
                        foreach (XmlElement element in nlist)
                        {
                            string org = element.GetElementsByTagName("Original")[0].InnerText;
                            string trans = element.GetElementsByTagName("Translation")[0].InnerText;

                            string s = regex.Replace(org, "");

                            if (string.IsNullOrEmpty(s))
                                continue;

                            orgCount++;

                            if (!string.IsNullOrEmpty(trans))
                                transCount++;

                        }

                        sensTotal += nlist.Count;
                        transTotal += transCount;
                        orgTotal += orgCount;

                        writer.WriteLine("<tr bgcolor='" + ((f = !f) ? "#EEEEFF" : "#FFFFFF") + "'>");
                        writer.WriteLine(string.Format(@"<td>{0}</td><td>{1}</td><td align='right'>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td><td align='right'>{5}</td>",
                            n++,
                            info.Name,
                            nlist.Count,
                            orgCount,
                            transCount,
                            transCount * 100 / orgCount + "%"
                        ));

                        writer.WriteLine("</tr>");
                    }


                    writer.WriteLine("<tr bgcolor='#C0C0FF'>");
                    writer.WriteLine(string.Format(@"<td>{0}</td><td>{1}</td><td align='right'>{2}</td><td align='right'>{3}</td><td align='right'>{4}</td><td align='right'>{5}</td>",
                        "Total",
                        "",
                        sensTotal,
                        orgTotal,
                        transTotal,
                        transTotal * 100 / orgTotal + "%"
                    ));

                    writer.WriteLine("</tr></table");

                    writer.WriteLine("<i>" + DateTime.Now.ToString() + "</i>");

                    writer.WriteLine("</body></html>");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
