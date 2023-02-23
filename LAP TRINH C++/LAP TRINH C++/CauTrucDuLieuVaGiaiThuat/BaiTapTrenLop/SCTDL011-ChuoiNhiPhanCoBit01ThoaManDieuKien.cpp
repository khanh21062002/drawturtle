#include <iostream>
#include <string>
using namespace std;

void generateBinaryStrings(int n, string str, bool& flag)
{
    if (n == 0) {
        if (!flag) {
            cout << str << endl;
        }
        return;
    }

    generateBinaryStrings(n - 1, str + "0", flag);
    if (str.size() < 1 || str.substr(str.size() - 1) != "1") {
        generateBinaryStrings(n - 1, str + "1", flag);
    }

    if (str.size() >= 2 && str.substr(str.size() - 2) == "01") {
        flag = true;
    }
}

int main()
{
    int n;
    cout << "Enter the value of N: ";
    cin >> n;

    string str = "";
    bool flag = false;
    generateBinaryStrings(n, str, flag);

    return 0;
}
