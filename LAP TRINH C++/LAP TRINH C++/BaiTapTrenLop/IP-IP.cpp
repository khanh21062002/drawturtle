#include<bits/stdc++.h>

using namespace std;
long long bd(string s){
	int n = s.size();
	long long k=0;
	for(int i=0;i<n;i++){
		k=k*10+(s[i]-48);
	} 
	return k;
}
bool check(string ip){
	 vector<string> ips;
    string ex = "";
    for (int i = 0; i < ip.size(); i++) {
        if (ip[i] == '.') {
            ips.push_back(ex);
            ex = "";
        }
        else {
            ex = ex + ip[i];
        }
    }
    ips.push_back(ex);
 
    for (int i = 0; i < ips.size(); i++) {
        if (ips[i].length() > 3|| bd(ips[i]) < 0 || bd(ips[i]) > 255)
            return false;
 
        if (ips[i].length() > 1 && bd(ips[i]) == 0)
            return false;
 
        if (ips[i].length() > 1&& bd(ips[i]) != 0&& ips[i][0] == '0')
            return false;
    }
    return true;
}
int main(){
	int t;
	cin >> t;
	cin.ignore();
	while(t--){
		string str;
		cin >> str;
		string l=str;
		int n = str.size(); 
		int D=0;
		for(int i=1;i<n-2;i++){
			for(int j=i+1;j<n-1;j++){
				for(int k=j+1;k<n;k++){
					l=l.substr(0,k)+"."+l.substr(k);
					l=l.substr(0,j)+"."+l.substr(j);
					l=l.substr(0,i)+"."+l.substr(i);
					if(check(l)){
						D++;
					}
					l = str;
				}
			}
		}
		cout << D << endl;
	}
}