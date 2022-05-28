#ifndef INTELHEXREADER_H
#define INTELHEXREADER_H

#include <iostream>
#include <fstream>
#include <sstream>
#include <cstring>
#include <iterator>
#include <string>
#include "Fileops.h"

using namespace std;

class IXReader
{
private:
  int    bytecounter; 
  int    datasize;
  int    exstrabytectr ;
  int    m_i;
  int    n;
  int    len;
  char*  code;
  string dataBytes;
public:
    IXReader(string file_contents);
    ~IXReader();
    bool IsStartOfLine();
    void GetDataSize();
    void WriteDataLines();
    void CountTillPageEnds();
    void CompleteThePage0xFF();
    void GenerateOutputFile();
    void TranslateAndGenerate();

};







#endif //INTELHEXREADER_H