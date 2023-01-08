#include <iostream>
#include <vector>

using namespace std;

// Hàm đệ quy để tìm tập con có k phần tử
void findSubsets(vector<int>& nums, vector<int>& subset, int k, int index) {
  // Nếu tập con hiện tại có đúng k phần tử, in ra tập con và tổng các phần tử trong nó
  if (subset.size() == k) {
    int sum = 0;
    for (int x : subset) {
      //cout << x << " ";
      sum += x;
    }
    cout << sum << endl;
    return;
  }

  // Nếu chúng ta đã duyệt hết mảng, trả về
  if (index == nums.size()) return;

  // Không chọn phần tử tại vị trí index
  findSubsets(nums, subset, k, index + 1);

  // Chọn phần tử tại vị trí index
  subset.push_back(nums[index]);
  findSubsets(nums, subset, k, index + 1);

  // Loại bỏ phần tử vừa chọn để tiếp tục tìm tập con khác
  subset.pop_back();
}

int main() {
    int n,k;
    cin >> n >> k;
    vector<int> nums;
    for(int i =0 ; i < n ;i++)
    {
        cin >> nums[i];
    }
    // cout << "All subsets of size " << k << " in array ";
    // for (int x : nums) cout << x << " ";
    // cout << ":" << endl;

    vector<int> subset;
    findSubsets(nums, subset, k, 0);

    return 0;
}
