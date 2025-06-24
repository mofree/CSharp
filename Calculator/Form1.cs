//整体设计参考vivo手机的计算器逻辑

namespace Calculator_winForm;

using System.Collections.Generic;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        //初始化
        line = "";
        label1.Text = "";
        flag_mark =  false;
    }
    
    public string line = "";
    public bool flag_mark = false;//为true时，其他符号会覆盖原符号
    
    private void btn_clear_Click(object sender, EventArgs e)
    {
        //和初始化部分代码一样
        line = "";
        label1.Text = "";
        flag_mark =  false;
    }
    
    

    public bool is_mark(char c)
    {
        if (c == '+' || c == '-' || c == '*' || c == '/') return true;
        return false;
    }
    
    
    public void line_add_char(char c)//按键按下后更新字符串
    {
        //用来初始化flag_mark
        if (line == "") flag_mark = false;
        
        if (is_mark(c))//是符号
        {
            //如果上一次输入也是符号
            if (flag_mark)
            {
                line = line.Substring(0, line.Length - 1) + c;
            }
            else
            {
                line = line + c;
            }
            flag_mark = true;
        }
        else//是数字
        {
            line += c;
            flag_mark = false;
        }
        
        label1.Text = line;
    }

    public string cal_formula(string str)//最终计算
    {
        //表达式无误则line变为结果，表达式有误则输出ERROR
        //注意-2,+2正确，但是*2,/2错误
        //如果运算式最后是一个符号则自动忽略该符号
        //！！除0错误如何检查？
        string s = str;

        if (s=="") return "";//长度为0
        else if (s.Length == 1)//长度为1
        {
            if (is_mark(s[0])) return "ERROR";
            else return line;
        }
        else//长度大于1
        {
            if (s[0]=='*' || s[0]=='/') return "ERROR";
            
            if (s[0] == '+' || s[0] == '-') s = '0' + str;//规范化，前面补充0
            if (is_mark(s[^1])) s=s[..^1];//规范化，去掉最后的符号
        }
        
        //完整计算式的计算逻辑，注意1+2*3=7
        //先处理乘除
        List<int> nums = new List<int>();//这里由于我用的int，那么如果计算结果得到小数后，是不能继续计算的
        List<char> marks = new List<char>();
        string temp_num_str = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (i == s.Length - 1)
            {
                temp_num_str += s[i];
                nums.Add(int.Parse(temp_num_str));
            }
            else if (char.IsDigit(s[i])) temp_num_str += s[i];
            else
            {
                marks.Add(s[i]);
                nums.Add(int.Parse(temp_num_str));
                temp_num_str = "";
            }
        }
        
        //先计算乘除
        List<double> nums2 = new List<double>();
        nums2.Add(nums[0]);
        List<char> marks2 = new List<char>();
        for (int i = 0; i < marks.Count; i++)
        {
            if (marks[i] == '+' || marks[i] == '-')
            {
                nums2.Add(nums[i+1]);
                marks2.Add(marks[i]);
            }
            else
            {
                double a = nums2[^1];
                nums2 = nums2[..^1];//stack_nums2.Pop();
                double b = nums[i + 1];
                if (marks[i] == '*') nums2.Add(a * b);
                else
                {
                    if (b==0) return "ERROR";
                    else nums2.Add(a/b);
                }
            }
        }
        
        //再计算加减
        double total = nums2[0];
        for (int i = 0; i < marks2.Count; i++)
        {
            if (marks2[i]=='+')  total += nums2[i+1];
            else total -= nums2[i+1];
        }
        
        return total.ToString();
    }
    
    private void btn_equal_Click(object sender, EventArgs e)
    {
        label1.Text = cal_formula(line);
        line = "";
    }

    private void btn_1_Click(object sender, EventArgs e)
    {
        //自：应该可以通过sender的prop来知道按钮按下的是哪一个
        line_add_char('1');
    }
    
    private void btn_2_Click(object sender, EventArgs e)
    {
        line_add_char('2');
    }

    private void btn_3_Click(object sender, EventArgs e)
    {
        line_add_char('3');
    }

    private void btn_4_Click(object sender, EventArgs e)
    {
        line_add_char('4');
    }

    private void btn_5_Click(object sender, EventArgs e)
    {
        line_add_char('5');
    }

    private void btn_6_Click(object sender, EventArgs e)
    {
        line_add_char('6');
    }

    private void btn_7_Click(object sender, EventArgs e)
    {
        line_add_char('7');
    }

    private void btn_8_Click(object sender, EventArgs e)
    {
        line_add_char('8');
    }

    private void btn_9_Click(object sender, EventArgs e)
    {
        line_add_char('9');
    }

    private void btn_0_Click(object sender, EventArgs e)
    {
        line_add_char('0');
    }


    private void btn_plus_Click(object sender, EventArgs e)
    {
        line_add_char('+');
    }

    private void btn_minus_Click(object sender, EventArgs e)
    {
        line_add_char('-');
    }

    private void btn_mul_Click(object sender, EventArgs e)
    {
        line_add_char('*');
    }

    private void btn_div_Click(object sender, EventArgs e)
    {
        line_add_char('/');
    }
}