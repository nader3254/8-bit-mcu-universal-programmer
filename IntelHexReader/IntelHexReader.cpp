
#include "IntelHexReader.h"
//#include "Fileops.h"

IXReader::IXReader(string file_contents )  : bytecounter(0)
                      ,exstrabytectr(0)
                      ,dataBytes("{\n")
                      ,m_i(0)
                      ,n(0)
{
    // n =0;
     n = file_contents.length();
 
    // declaring character array
    char codetemp[n + 1];
    code =new char[n+1];

    // copying the contents of the
    // string to char array
    strcpy(codetemp, file_contents.c_str());
    len =sizeof(codetemp);
    for (int i = 0; i <sizeof(codetemp); i++)
    {
        code[i] = codetemp[i];

    }
    /*
    cout<<code<<endl;
     cout<<sizeof(code)<<endl;
  cout<<sizeof(codetemp)<<endl;
   cout<<code[11]<<endl;
  */  
}

IXReader::~IXReader()
{
}

 void IXReader::TranslateAndGenerate()
 {
      cout<<code<<endl;
      
    for(int i=0;i<len;i++)
       {
           
           m_i=i;
          if( IsStartOfLine())
          {
                GetDataSize();
                WriteDataLines();

          }
       
       }
        
    CountTillPageEnds() ;
    CompleteThePage0xFF();
    cout<<dataBytes<<endl;
 
  
    GenerateOutputFile();
    
    Fileops* log = new Fileops();
    log->NewFile("history.log");
    string msg ="output file generated succesfully ... ";
    log->WriteFile(msg,"history.log"); 

 }

bool IXReader::IsStartOfLine()
{
    if(code[m_i]==':')
        return true;
    else
        return false;    

}


void  IXReader::GetDataSize()
{
         string sz;
          sz += code[m_i+1] ;
          sz += code[m_i+2] ;
          stringstream ss ;
          ss << hex << sz;
          ss >> datasize;

}


void IXReader::WriteDataLines()
{
     for (int j = (m_i+9); j <(m_i+9+(datasize*2)) ; j=j+2)
          {  
              bytecounter++;
              dataBytes += "0x";
              dataBytes += code[j];
              dataBytes += code[j+1];
              dataBytes +=", ";
          }
          
          dataBytes += '\n';

}
void IXReader::CountTillPageEnds()
{
     while ((bytecounter % 128 )!=1)
     {
         exstrabytectr++;
         bytecounter++;
         
     }
     exstrabytectr--;
     bytecounter--;

}
void IXReader::CompleteThePage0xFF()
{
    for (int i=(bytecounter-exstrabytectr);i<bytecounter;i++)
     {
           dataBytes += "0xFF";
           if (i!=bytecounter-1)
           {  
               dataBytes +=", ";
           }
           
         
           if (i % 16==true)
           {
              dataBytes += '\n';
           }
           
     }
     dataBytes += "\n}";

}
void IXReader::GenerateOutputFile()
{
     int pageno = bytecounter /128;
     stringstream ss ;
     ss << pageno;
     string page; ss >> page;
     stringstream s2 ;
     s2 << bytecounter;
     string bytes; s2 >> bytes;
     Fileops * outputfile =new Fileops();
     outputfile->NewFile("outputfile.text");
     string temp = "output code is \n"+ dataBytes +"\n no of pages is : " +page + "\n no of bytes is :" + bytes+"\n ";
     outputfile->WriteFile(temp,"outputfile.text");  
}