#include<bits/stdc++.h>

using namespace std;

char upper(char n){
    if('a' <= n && n <= 'z'){
        n = n-((int)('a')-(int)('A'));
    }
    return n;
}

char lower(char n){
    if('A' <= n && n <= 'Z'){
        n = n + ((int)('a') - (int)('A'));
    }
    return n;
}

int main(){
    char chr;
    cin >> chr;
    cout << "lower: "<< lower(chr) << endl;
    cout << "upper: " << upper(chr) << endl;
    return 0;
}