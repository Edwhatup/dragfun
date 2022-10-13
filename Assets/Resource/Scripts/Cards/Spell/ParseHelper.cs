using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParseHelper
{
    public static int ParseCardIntWithMessage(string s, string cardId, string cardName)
    {
        int res;
        if (!int.TryParse(s, out res))
        {
            Debug.LogError($"创建id为{cardId}，名称为{cardName}的卡牌时参数出错，{s}无法转型为int类型");
        }
        return res;
    }
    public static void CheckParamsCountWithMessage(string[] paras, int expected, string cardid, string cardName)
    {
        if (paras.Length < expected) 
            Debug.LogError($"id为{cardid},name为{cardName}的卡牌类型缺少对应参数，期望{expected}，实际{paras.Length}");
    }
}
