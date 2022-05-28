#ifndef FILEOPS_H
#define FILEOPS_H

#include <iostream>
#include <fstream>
#include <sstream>
#include <cstring>
#include <iterator>
#include <string>

using namespace std;


#ifndef __cplusplus	
      extern "c"{
#endif 

class Fileops
{
private:
    
public:

    Fileops(/* args */);
    ~Fileops();
   void NewFile(string path );
   void WriteFile(string data,string filepath);
   string ReadFile(string path);
   


};

#ifndef __cplusplus	
        }
#endif 


#endif //FILEOPS_H


