#include<bits/stdc++.h>

using namespace std;

int main(int argc, char const *argv[])
{
	int t;	cin >> t;
	cin.ignore();
	while(t--){
		string s;	getline(cin, s);
		if(s[9] == '"' || s[s.size() - 2] == '"'){
			cout << "0" << endl;
		}
		else{
			int pos = s.find(",");
			string tmp1 = s.substr(9, pos - 10);
			string tmp2 = s.substr(pos + 12, s.size()- pos - 13);
			if(tmp2.find(tmp1) != -1)	cout << tmp2.find(tmp1) << endl;
			else						cout << "-1" << endl;
		}
	}
	return 0;
}