#include "Fileops.h"


Fileops::Fileops(/* args */)
{
}

Fileops::~Fileops()
{
}

void Fileops :: NewFile(string path)
{  
    
    
    ofstream outfile (path);
    outfile.close();
   
}
void Fileops ::WriteFile(string data ,string filepath)
{
    ofstream outfile (filepath);

     outfile << data <<endl;

     outfile.close();

}
string Fileops ::ReadFile(string path)
{
    auto ss = ostringstream{};
    ifstream input_file(path);
    if (!input_file.is_open()) {
        cerr << "Could not open the file - '"
             << path << "'" << endl;
         NewFile("history.log");
         string msg ="Could not open the file :: "+path;
         WriteFile(msg,"history.log")   ; 
           exit(EXIT_FAILURE);

    }
    ss << input_file.rdbuf();
    return ss.str();
}