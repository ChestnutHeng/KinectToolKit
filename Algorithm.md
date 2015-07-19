﻿#Kinect模式识别算法设计
人的动作识别问题其实是一个模式识别问题，所以待解决的部分为特征提取和分类器的设计。

------
##人体特征提取
人体特征提取的主要方式为提取特征点、识别模型和形状信息的分析。[1]
###静态方法
静态方法有形状识别，提取特征点和识别模型三种。
>* 形状识别：人类的自然视觉识别系统即为该识别模式，不需要对人体进行建模。主要信息库中有高宽比，紧密度，周长，面积等。由于该识别方法必须使用完整的人体信息，不能存在缺失，所以不能在动作识别中使用。对应的识别方法为KinectColorView。
>* 提取特征点：在二维图像识别中，有些点的位置坐标显得极为重要，通过其坐标获取就可以获得人的大致运动数据，我们称之为时空兴趣点。对应的识别方法为KinectSkeletonView。
>* 识别模型：在特征点的基础上，我们将人体运动点坐标位移建模，之后的读取中对之进行坐标比对，来完成人体运动的重建。由于Kinect自身的特性，我们可以直接获得三维坐标（ColorView和DepthView的信息综合），使用20个骨架点对之进行建模。
人的关节点静态动作模型便可以定义为
$$G_f(T_a T_b T_c:R_a R_b R_c)$$
其中T表示点在三维欧式空间中的平移，R表示点在三维欧式空间中的旋转。

###动态方法
>* 人的动作过程具体体现为视频流图像，每一帧图像包含了一个静态姿态。于是静态姿态下的标量$G_f$随着时间的推移就成为了一组向量$\overrightarrow{G_f}$。
定义列向量$A_1$ = {${G_{f1},G_{f2},...,G_{fn}}$}，其中$G_{fn}$为对应的每帧的静态关节点位置。
定义人体运动过程向量：
$$AF = \{A_1,A_2,A_3,...,A_{20}\}$$


在中期以前的识别算法中，本小组主要用静态方法的前两种进行动作的识别，存在匹配率低下、识别不稳定等多种问题，在中期以后改用动作识别模型进行匹配，极大地提升了准确率。



------

##分类器设计
分类器设计主要有以下三种：
>* 模板匹配，一般的通用实现为模板比对和DP。
>* 概率统计，实现为贝叶斯网络和隐马尔科夫模型。
>* 语法方法，实现为有限状态机。

模板匹配：我们可以把运动模型的数据和模板数据进行直接比对，计算累积误差*det* A ，寻找最低误差的匹配。该算法实现简单，但是缺点是无法解决运动速率快慢的问题，匹配样本有极大误差。
语法方法：使用有限状态机建模，通过语法推理的方法识别动作。该方法的缺点是，一方面方法是人为产生的，无法准确描述动态系统，另一方面他不能通过样本学习得到，不体现样本的规律。
概率方法：主要有HMM，DBN，CRF等方法，识别精度很高，但是由于过于复杂，所以不在本次设计中使用。

在中期以前的动作识别中，主要使用第二种方法（有限状态机）进行识别，通过极大的动作差异和大识别域的静态识别方法简化了代码，但在识别精度上存在不少问题，会有程序卡出和识别错误产生。
在中期以后的动作识别中，通过学习模式识别相关资料，决定采用优化的DP进行模板匹配。
###算法原理
DTW算法（Dynamic Time Warping）起源于语音识别中，用于解决发音长短不同的语音匹配问题具体做法为拉伸识别序列或者模板序列中的一个时间轴，来使得动作重叠度达到最大。

设对动作序列A和B进行比较：
$$A = \{q_1,q_2,q_3,...,q_n\}$$
$$B  = \{p_1,p_2,p_3,...,p_m\}$$
不妨建立距离矩阵n*m，使得(i,j)元素为$q_i$和$c_j$的距离。
定义规整路径$$W = \{w_1,w_2,...,w_k,...,w_K\}~~~~~其中 max(m,n)\leq K \leq m+n-1$$
有以下约束：
>* 边界约束 起点(1,1) 终点(m,n)
>* 连续约束 只允许步长为相邻的元素
>* 单调约束 点在时间上是单调的，只能朝一个方向走。

通过这些约束，我们就可以把规整路径W视为一个从集合A到B的帧映射。

则路径长度$$W=min\{\sqrt{
\begin{equation*}
 \sum_{k=1}^Kk_n
\end{equation*}}/K
\}$$
K 用于补偿路径。不妨采用动态规划求出最短路，写出状态转移方程：
$$r(i,j)=d(q_i,p_j)+min\{r(i-1,j-1),r(i-1,j),r(i,j-1)\}$$

------
###坐标优化
选取臀部坐标作为原点建立坐标系
定义节点坐标Class Temp_RelativeSkeletonPiont {X,Y,Z}
         = s.Joints[JointType.Temp].Position - s.Joints[JointType.Spine].Position
为相应的相对坐标。
对应矢量$\overrightarrow{T} = \{X,Y,Z\}$ ，对应坐标 （X,Y,Z）。
加上原点后有20个向量，定义向量组 R =  {S1，S2，...，Sn} 为一个静态动作特征矢量。

###模板与测试样本的建立

模板建立：
对于某一动作：
选取任意骨骼节点S，记录每一帧下坐标（X，Y，Z），设动作持续n帧。则得到列向量$\{s_1,s_2,...,s_n\}$。
同样对其他19个骨骼节点也做该处理。得到20个列向量，组成20*n的矩阵。

样本建立：
采用和模板建立同样的方法，只不过由于动作快慢不同，设持续n帧，组成20*m的矩阵。

###规整路线
由于共有20个节点，如果对每个节点都做一个动态规整，维数会上升到三维，效率会大幅降低。不妨选取感兴趣的关键点（如伸手动作的肩、手、和肘部，弯腰的头、腰和脚），定义不同样本间的点点距离为关键点之间欧式距离的和，即
*$$D(A,B)=\{{
\begin{equation*}
 \sum_{i=1}^{20}S_i
\end{equation*}}
\}$$*
其中S为每个点在不同样本间的欧式距离，
*$$S_i = \sqrt{(A_i.X - B_i.X)^2 + (A_i.Y - B_i.Y)^2+(A_i.Z - B_i.Z)^2}$$*

创建规整矩阵n*m，其中的(i,j)的值为对应的i帧时模板点点到j帧时样本点的欧式距离。
$$D(i,j)=S_i$$
然后搜索出最短路
$$r(i,j)=D(i,j)+min\{r(i-1,j-1),r(i-1,j),r(i,j-1)\}$$
 
###模板匹配
在对模板库中每一个模板都进行比对搜索之后，记录每个模板路径的误差值R，并从中找出最小值$R_{min}$即可。对应的模板即为我们匹配成功的模板。

至此，本章节介绍了一个可以使用的动作识别算法。

------


