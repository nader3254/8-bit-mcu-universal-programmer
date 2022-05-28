#include <iostream>
#include <fstream>
#include <sstream>
#include <cstring>
#include <iterator>
#include <string>


#include "Fileops.h"
#include "IntelHexReader.h"

using namespace std;




int main()
{
  
 Fileops*  pathconf  =new Fileops();
 IXReader* hex2array =new IXReader(pathconf->ReadFile(pathconf->ReadFile("path.conf")));
 hex2array->TranslateAndGenerate();




 
    

}

    /* test done 
       cout<<"line no : "<<i<<endl;
        cout<< bytesize<<endl;
       */ 

       /*  test for convertig string to char array 
   
    for (int i = 0; i < n; i++)
        cout << char_array[i]<<endl;

*/
// cout<<stoi(sz)<<endl;







/*
string readFileIntoString2(const string& path) {
    auto ss = ostringstream{};
    ifstream input_file(path);
    if (!input_file.is_open()) {
        cerr << "Could not open the file - '"
             << path << "'" << endl;
    
       
    }
    ss << input_file.rdbuf();
    return ss.str();
}


void creatFille(string code)
{
   ofstream outfile ("outputfile.text");

   outfile << code <<endl;

     outfile.close();


}
*/






/*
   //("F:\\BLD_App.hex")
    
    string filename;
   //cout<<"enter the file path :: \n";
    string file_contents;
    filename= readFileIntoString2("path.conf");
   // cout<<filename<<endl;
   
    file_contents = readFileIntoString2(filename);
    cout << file_contents << endl;

     int n =0;
     n = file_contents.length();
 
    // declaring character array
    char code[n + 1];
 
    // copying the contents of the
    // string to char array
    strcpy(code, file_contents.c_str());

 //page =128 byte

  int bytecounter=0; 
  int datasize=0;int exstrabytectr = 0;
  string dataBytes = "{\n";

  for(int i=0;i<sizeof(code);i++)
  {
      if(code[i]==':')
      {
          
          string sz;
          sz += code[i+1] ;
          sz += code[i+2] ;
          stringstream ss ;
          ss << hex << sz;
          ss >> datasize;

          for (int j = (i+9); j <(i+9+(datasize*2)) ; j=j+2)
          {  
              bytecounter++;
              dataBytes += "0x";
              dataBytes += code[j];
              dataBytes += code[j+1];
              dataBytes +=", ";
          }
          
          dataBytes += '\n';
      }
   }
    
   // cout << dataBytes<<endl;
    cout<< "byte counter : "<<bytecounter<<endl;

     while ((bytecounter % 128 )!=1)
     {
         exstrabytectr++;
         bytecounter++;
         
     }
     exstrabytectr--;
     bytecounter--;
     cout<< "byte counter : "<<bytecounter<<endl;
     cout<< "extra byte counter : "<<exstrabytectr<<endl;
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
     cout << dataBytes<<endl;
     int pageno = bytecounter /128;
     stringstream ss ;
     ss << pageno;
     string page; ss >> page;
     stringstream s2 ;
     s2 << bytecounter;
     string bytes; s2 >> bytes;


     creatFille("output code is \n"+ dataBytes +"\n no of pages is : " +page + "\n no of bytes is :" + bytes+"\n ");

   // while (1);                 // wait here 
   
     
*/