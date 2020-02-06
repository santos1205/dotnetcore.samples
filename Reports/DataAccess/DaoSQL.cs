using Proseg.Dao;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess
{
    class DaoSQL : SQLServer
    {
        public DaoSQL() : this("configConnetion.ini") { }

        public DaoSQL(String arquivoIni)
        {
            //leitura do arquivo do arquivo de inicialização do banco
            StreamReader objReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/Bin/" + arquivoIni);
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();

            this.StrDataBaseName = arrText[0].ToString().Replace("base=", "");
            this.StrServer = arrText[1].ToString().Replace("serverdb=", "");
            this.StrLogin = arrText[2].ToString().Replace("login=", "");
            this.StrPassword = arrText[3].ToString().Replace("senha=", "");
        }
    }
}
