#include<bits/stdc++.h>
using namespace std;

string binaryToGray(string binary) {
    string gray = "";
    gray += binary[0];

    for (int i = 1; i < binary.length(); i++) {
        if (binary[i] != binary[i-1]) {
            gray += "1";
        } else {
            gray += "0";
        }
    }

    return gray;
}

int main() {
    int n = 3;
    string binary = "010";
    string gray = binaryToGray(binary);
    cout << "Binary: " << binary << endl;
    cout << "Gray: " << gray << endl;
    return 0;
}
