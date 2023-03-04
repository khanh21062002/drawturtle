#include <iostream>
#include <vector>
using namespace std;

const int MAX = 1005;

int T, N, u, v;
vector<int> adj[MAX];
bool visited[MAX];

bool dfs(int u, int parent) {
    visited[u] = true;
    for (int v : adj[u]) {
        if (!visited[v]) {
            if (!dfs(v, u))
                return false;
        } else if (v != parent) {
            return false;
        }
    }
    return true;
}

bool is_tree() {
    // Duyệt đồ thị bắt đầu từ đỉnh 1
    if (!dfs(1, -1))
        return false;
    // Kiểm tra số lượng cạnh
    int cnt_edges = 0;
    for (int i = 1; i <= N; i++) {
        cnt_edges += adj[i].size();
    }
    cnt_edges /= 2; // mỗi cạnh được đếm 2 lần
    return cnt_edges == N - 1;
}

int main() {
    cin >> T;
    while (T--) {
        cin >> N;
        for (int i = 1; i <= N; i++) {
            adj[i].clear();
            visited[i] = false;
        }
        for (int i = 1; i < N; i++) {
            cin >> u >> v;
            adj[u].push_back(v);
            adj[v].push_back(u);
        }
        // Kiểm tra đồ thị có phải là cây hay không
        if (is_tree())
            cout << "YES\n";
        else
            cout << "NO\n";
    }
    return 0;
}
