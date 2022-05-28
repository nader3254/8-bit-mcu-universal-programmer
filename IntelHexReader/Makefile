run: clean
	g++ -c FileOps.cpp -o FileOps.o
	g++ -c IntelHexReader.cpp -o IntelHexReader.o
	ar rcs libfileops.a FileOps.o
	ar rcs libIntelHexReader.a IntelHexReader.o
	g++ -c main.cpp 
	g++ -o bin main.o -L. -lIntelHexReader -lFileOps   -v 

clean:
	rm FileOps.o
	rm libfileops.a
	rm IntelHexReader.o
	rm libIntelHexReader.a
	rm main.o
	rm bin.exe