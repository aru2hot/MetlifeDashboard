using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SLACalc
/// </summary>
public class SLACalc
{
    // Variables needed for calculation
    private int no_of_def_req;
    private int no_of_req;
    private int Cr_Count;
    private int des_artifacts_reviewd;
    private int total_des_artifacts;
    private int code_reviewd;
    private int total_code_components;
    private int unit_tested_components;
    private int del_on_time;
    private int total_deliverables;
    private int unit_test_passed;
    private int total_unit_test;
    private int phase_seq;

    // Variables to find if the SLA Metric Is Applicable or not 

    public bool req_quality_score_applicable;

    public bool req_stab_applicable;

    public bool des_rev_cov_score_applicable;

    public bool code_rev_cov_score_applicable;

    public bool unit_test_cov_score_applicable;

    public bool on_time_del_score_applicable;

    public bool delivery_defect_density_score_applicable;


    // Public Variables - SLA METRIC VALUES

    public int req_quality_score;
    public int req_quality_value;
    public int req_stab_score;
    public int req_stab_value;
    public int des_rev_cov_score;
    public int des_rev_cov_value;
    public int code_rev_cov_score;
    public int code_rev_cov_value;
    public int unit_test_cov_score;
    public int unit_test_cov_value;
    public int on_time_del_score;
    public int on_time_del_value;
    public int delivery_defect_density_score;
    public int delivery_defect_density_value;


    //NON PARAM CONSTRUCTOR
    public SLACalc()
    {

    }
    // PARAM CONSTRUCTOR
    public SLACalc(int no_of_def_req_tmp, int no_of_req_tmp, int cr_count_tmp, int des_arti_reviewed_tmp, int total_des_artifacts_tmp, int code_reviewed_tmp, int total_code_comp_tmp, int unit_test_comp, int del_on_time_tmp, int total_del, int unit_test_passed_tmp, int total_unit_test_tmp, int phase_seq_tmp)
    {
        no_of_def_req = no_of_def_req_tmp;
        no_of_req = no_of_req_tmp;
        Cr_Count = cr_count_tmp;
        des_artifacts_reviewd = des_arti_reviewed_tmp;
        total_des_artifacts = total_des_artifacts_tmp;
        code_reviewd = code_reviewed_tmp;
        total_code_components = total_code_comp_tmp;
        unit_tested_components = unit_test_comp;
        del_on_time = del_on_time_tmp;
        total_deliverables = total_del;
        unit_test_passed = unit_test_passed_tmp;
        total_unit_test = total_unit_test_tmp;
        phase_seq = phase_seq_tmp;

        // SETTING THE SCORE AND VALUE 
        req_quality_score = CalculateReqQuality(no_of_def_req, no_of_req, phase_seq, req_quality_score_applicable).Item2;
        req_quality_value = CalculateReqQuality(no_of_def_req, no_of_req, phase_seq, req_quality_score_applicable).Item1;
        req_stab_score = CalculateReqStability(Cr_Count, no_of_req, phase_seq, req_stab_applicable).Item2;
        req_stab_value = CalculateReqStability(Cr_Count, no_of_req, phase_seq, req_stab_applicable).Item1;
        des_rev_cov_score = CalculateDesignReviewCoverage(des_artifacts_reviewd, total_des_artifacts, phase_seq, des_rev_cov_score_applicable).Item2;
        des_rev_cov_value = CalculateDesignReviewCoverage(des_artifacts_reviewd, total_des_artifacts, phase_seq, des_rev_cov_score_applicable).Item1;
        code_rev_cov_score = CalculateCodeReviewCoverage(code_reviewd, total_code_components, phase_seq, code_rev_cov_score_applicable).Item2;
        code_rev_cov_value = CalculateCodeReviewCoverage(code_reviewd, total_code_components, phase_seq, code_rev_cov_score_applicable).Item1;
        unit_test_cov_score = CalculateUnitTestCoverage(unit_tested_components, total_code_components, phase_seq, unit_test_cov_score_applicable).Item2;
        unit_test_cov_value = CalculateUnitTestCoverage(unit_tested_components, total_code_components, phase_seq, unit_test_cov_score_applicable).Item1;
        on_time_del_score = CalculateOnTimeDelivery(del_on_time, total_deliverables, phase_seq, on_time_del_score_applicable).Item2;
        on_time_del_value = CalculateOnTimeDelivery(del_on_time, total_deliverables, phase_seq, on_time_del_score_applicable).Item1;
        delivery_defect_density_score = CalculateDelDefectDensity(unit_test_passed, total_unit_test, phase_seq, delivery_defect_density_score_applicable).Item2;
        delivery_defect_density_value = CalculateDelDefectDensity(unit_test_passed, total_unit_test, phase_seq, delivery_defect_density_score_applicable).Item1;

    }

    public SLACalc(List<int> list1, List<string> list2)
    {
        // TODO: Complete member initialization


        no_of_def_req = list1[0];
        no_of_req = list1[1];
        Cr_Count = list1[2];
        des_artifacts_reviewd = list1[3];
        total_des_artifacts = list1[4];
        code_reviewd = list1[5];
        total_code_components = list1[6];
        unit_tested_components = list1[7];
        del_on_time = list1[8];
        total_deliverables = list1[9];
        unit_test_passed = list1[10];
        total_unit_test = list1[11];
        phase_seq = list1[12];

        // setting the isApplicable values 
        req_quality_score_applicable = (list2[0].ToString() == "Yes") ? true : false;

        req_stab_applicable = (list2[1].ToString() == "Yes") ? true : false;

        des_rev_cov_score_applicable = (list2[2].ToString() == "Yes") ? true : false;

        code_rev_cov_score_applicable = (list2[3].ToString() == "Yes") ? true : false;

        unit_test_cov_score_applicable = (list2[4].ToString() == "Yes") ? true : false;

        on_time_del_score_applicable = (list2[5].ToString() == "Yes") ? true : false;

        delivery_defect_density_score_applicable = (list2[6].ToString() == "Yes") ? true : false;


        // SETTING THE SCORE AND VALUE 
        req_quality_score = CalculateReqQuality(no_of_def_req, no_of_req, phase_seq, req_quality_score_applicable).Item2;
        req_quality_value = CalculateReqQuality(no_of_def_req, no_of_req, phase_seq, req_quality_score_applicable).Item1;
        req_stab_score = CalculateReqStability(Cr_Count, no_of_req, phase_seq, req_stab_applicable).Item2;
        req_stab_value = CalculateReqStability(Cr_Count, no_of_req, phase_seq, req_stab_applicable).Item1;
        des_rev_cov_score = CalculateDesignReviewCoverage(des_artifacts_reviewd, total_des_artifacts, phase_seq, des_rev_cov_score_applicable).Item2;
        des_rev_cov_value = CalculateDesignReviewCoverage(des_artifacts_reviewd, total_des_artifacts, phase_seq, des_rev_cov_score_applicable).Item1;
        code_rev_cov_score = CalculateCodeReviewCoverage(code_reviewd, total_code_components, phase_seq, code_rev_cov_score_applicable).Item2;
        code_rev_cov_value = CalculateCodeReviewCoverage(code_reviewd, total_code_components, phase_seq, code_rev_cov_score_applicable).Item1;
        unit_test_cov_score = CalculateUnitTestCoverage(unit_tested_components, total_code_components, phase_seq, unit_test_cov_score_applicable).Item2;
        unit_test_cov_value = CalculateUnitTestCoverage(unit_tested_components, total_code_components, phase_seq, unit_test_cov_score_applicable).Item1;
        on_time_del_score = CalculateOnTimeDelivery(del_on_time, total_deliverables, phase_seq, on_time_del_score_applicable).Item2;
        on_time_del_value = CalculateOnTimeDelivery(del_on_time, total_deliverables, phase_seq, on_time_del_score_applicable).Item1;
        delivery_defect_density_score = CalculateDelDefectDensity(unit_test_passed, total_unit_test, phase_seq, delivery_defect_density_score_applicable).Item2;
        delivery_defect_density_value = CalculateDelDefectDensity(unit_test_passed, total_unit_test, phase_seq, delivery_defect_density_score_applicable).Item1;

    }

    public Tuple<int, int> CalculateReqQuality(int no_of_def_req, int no_of_req, int phase_seq, bool is_applicable)
    {
        //Checking if the SLA is applicable from User Input
        if (!is_applicable)
            return Tuple.Create(-4, -4);
        // calcualate scenarios where there is no Requirement or no defects in requirement
        if (phase_seq < 0)
            return Tuple.Create(-3, -3); // Not Applicabale from Phase Sequence POV = -3
        if (no_of_def_req == 0 && no_of_req != 0)
        {
            return Tuple.Create(0, 5); ;
        }
        else if (no_of_def_req == 0 && no_of_req == 0)
        {
            return Tuple.Create(-1, -1); ; // Both are 0 
        }
        else if (no_of_def_req != 0 && no_of_req != 0)
        {
            int val = Convert.ToInt32(Math.Round((Convert.ToDouble(no_of_def_req) / Convert.ToDouble(no_of_req)), 2) * 100);
            int score = CalculateReqQualityScore(val);

            return Tuple.Create(val, score);


        }
        // If everything fails which is unlikely
        return Tuple.Create(-2, -2);

    }
    public Tuple<int, int> CalculateReqStability(int Cr_Count, int no_of_req, int phase_seq, bool is_applicable)
    {
        //Checking if the SLA is applicable from User Input
        if (!is_applicable)
            return Tuple.Create(-4, -4);
        // calcualate scenarios where there is no Requirement or no defects in requirement
        if (phase_seq < 0)
            return Tuple.Create(-3, -3); // Not Applicabale = -3
        if (Cr_Count == 0 && no_of_req != 0)
        {
            // no CR 
            return Tuple.Create(0, 5);
        }
        else if (Cr_Count == 0 && no_of_req == 0)
        {
            // initial phase ( no requirements identified Yet ) 
            return Tuple.Create(-1, -1);
        }
        else if (Cr_Count != 0 && no_of_req != 0)
        {
            int val = Convert.ToInt32(Math.Round((Convert.ToDouble(Cr_Count) / Convert.ToDouble(no_of_req)), 2) * 100);
            int score = CalculateReqStabilityScore(val);

            return Tuple.Create(val, score);   
        }
        // If everything fails which is unlikely
        return Tuple.Create(-2, -2);
    }
    public Tuple<int, int> CalculateDesignReviewCoverage(int des_artifacts_reviewd, int total_des_artifacts, int phase_seq, bool is_applicable)
    {
        //Checking if the SLA is applicable from User Input
        if (!is_applicable)
            return Tuple.Create(-4, -4);

        // calcualate scenarios where there is no Requirement or no defects in requirement
        if (phase_seq < 3)
            return Tuple.Create(-3, -3); // Not Applicabale = -3

        if (des_artifacts_reviewd == 0 && total_des_artifacts != 0)
        {
            // no CR 
            return Tuple.Create(0, 1);
        }
        else if (des_artifacts_reviewd == 0 && total_des_artifacts == 0)
        {
            // initial phase ( no requirements identified Yet ) 
            return Tuple.Create(-1, -1);
        }
        else if (des_artifacts_reviewd != 0 && total_des_artifacts != 0)
        {
            int val = Convert.ToInt32(Math.Round((Convert.ToDouble(des_artifacts_reviewd) / Convert.ToDouble(total_des_artifacts)), 2) * 100);
            int score = CalculateDesignReviewCoverageScore(val);
            return Tuple.Create(val, score);
        }
        // If everything fails which is unlikely
        return Tuple.Create(-2, -2);
    }
    public Tuple<int, int> CalculateCodeReviewCoverage(int code_reviewd, int total_code_components, int phase_seq, bool is_applicable)
    {
        //Checking if the SLA is applicable from User Input
        if (!is_applicable)
            return Tuple.Create(-4, -4);

        // calcualate scenarios where there is no Requirement or no defects in requirement
        if (phase_seq < 4)
            return Tuple.Create(-3, -3); // Not Applicabale = -3
        if (code_reviewd == 0 && total_code_components != 0)
        {
            // no CR 
            return Tuple.Create(0, 1);
        }
        else if (code_reviewd == 0 && total_code_components == 0)
        {
            // initial phase ( no requirements identified Yet ) 
            return Tuple.Create(-1, -1);
        }
        else if (code_reviewd != 0 && total_code_components != 0)
        {
            int val = Convert.ToInt32(Math.Round((Convert.ToDouble(code_reviewd) / Convert.ToDouble(total_code_components)), 2) * 100);
            int score = CalculateCodeReviewCoverageScore(val);

            return Tuple.Create(val, score);
        }
        // If everything fails which is unlikely
        return Tuple.Create(-2, -2);
    }
    public Tuple<int, int> CalculateUnitTestCoverage(int unit_tested_components, int total_code_components, int phase_seq, bool is_applicable)
    {
        //Checking if the SLA is applicable from User Input
        if (!is_applicable)
            return Tuple.Create(-4, -4);

        // calcualate scenarios where there is no Requirement or no defects in requirement
        if (phase_seq < 4)
            return Tuple.Create(-3, -3); // Not Applicabale = -3
        if (unit_tested_components == 0 && total_code_components != 0)
        {
            // no CR 
            return Tuple.Create(0, 1);
        }
        else if (unit_tested_components == 0 && total_code_components == 0)
        {
            // initial phase ( no requirements identified Yet ) 
            return Tuple.Create(-1, -1); ;
        }
        else if (unit_tested_components != 0 && total_code_components != 0)
        {
            int val = Convert.ToInt32(Math.Round((Convert.ToDouble(unit_tested_components) / Convert.ToDouble(total_code_components)), 2) * 100);
            int score = CalculateUnitTestCoverageScore(val);
            return Tuple.Create(val, score);
        }
        // If everything fails which is unlikely
        return Tuple.Create(-2, -2);
    }
    public Tuple<int, int> CalculateOnTimeDelivery(int del_on_time, int total_deliverables, int phase_seq, bool is_applicable)
    {

        //Checking if the SLA is applicable from User Input
        if (!is_applicable)
            return Tuple.Create(-4, -4);

        // calcualate scenarios where there is no Requirement or no defects in requirement
        if (phase_seq < 6)
            return Tuple.Create(-3, -3); // Not Applicabale = -3
        if (del_on_time == 0 && total_deliverables != 0)
        {
            // no Del On Time 
            return Tuple.Create(0, 1);
        }
        else if (del_on_time == 0 && total_deliverables == 0)
        {
            // initial phase ( no requirements identified Yet ) 
            return Tuple.Create(-1, -1);
        }
        else if (del_on_time != 0 && total_deliverables != 0)
        {
            int val = Convert.ToInt32(Math.Round((Convert.ToDouble(del_on_time) / Convert.ToDouble(total_deliverables)), 2) * 100);
            int score = CalculateOnTimeDeliveryScore(val);
            return Tuple.Create(val, score);
        }
        // If everything fails which is unlikely
        return Tuple.Create(-2, -2);
    }
    public Tuple<int, int> CalculateDelDefectDensity(int unit_test_passed, int total_unit_test, int phase_seq, bool is_applicable)
    {
        //Checking if the SLA is applicable from User Input
        if (!is_applicable)
            return Tuple.Create(-4, -4);

        // calcualate scenarios where there is no Requirement or no defects in requirement
        if (phase_seq < 4)
            return Tuple.Create(-3, -3); ; // Not Applicabale = -3
        if (unit_test_passed == 0 && total_unit_test != 0)
        {
            // no Del On Time 
            return Tuple.Create(0, 1);
        }
        else if (unit_test_passed == 0 && total_unit_test == 0)
        {
            // initial phase ( no requirements identified Yet ) 
            return Tuple.Create(-1, -1);
        }
        else if (unit_test_passed != 0 && total_unit_test != 0)
        {
            int val = Convert.ToInt32(Math.Round((Convert.ToDouble(unit_test_passed) / Convert.ToDouble(total_unit_test)), 2) * 100);
            int score = CalculateDelDefectDensityScore(val);
            return Tuple.Create(val, score);

        }
        // If everything fails which is unlikely
        return Tuple.Create(-2, -2);
    }


    //Score Calculations Begin
    public int CalculateDelDefectDensityScore(int val)
    {
        int score = 0;

        //calculate the Score
        if (val <= 87)
            score = 1;
        else if (val >= 88 && val <= 89)
            score = 2;
        else if (val == 90)
            score = 3;
        else if (val >= 91 && val <= 92)
            score = 4;
        else if (val >= 93 && val <= 100)
            score = 5;

        return score;
    }

    public int CalculateReqQualityScore(int val)
    {
        int score = 0;

        //calculate the Score
        if (val >= 85)
            score = 1;
        else if (val >= 77 && val <= 84)
            score = 2;
        else if (val == 76)
            score = 3;
        else if (val >= 68 && val <= 75)
            score = 4;
        else if (val >= 0 && val <= 67)
            score = 5;

        return score;
    }

    public int CalculateReqStabilityScore(int val)
    {
        int score = 0;

        //calculate the Score
        if (val >= 41)
            score = 1;
        else if (val >= 37 && val <= 40)
            score = 2;
        else if (val == 36)
            score = 3;
        else if (val >= 32 && val <= 35)
            score = 4;
        else if (val >= 0 && val <= 31)
            score = 5;

        return score;
    }

    public int CalculateOnTimeDeliveryScore(int val)
    {
        int score = 0;

        //calculate the Score
        if (val <= 87)
            score = 1;
        else if (val >= 88 && val <= 89)
            score = 2;
        else if (val == 90)
            score = 3;
        else if (val >= 91 && val <= 92)
            score = 4;
        else if (val >= 93 && val <= 100)
            score = 5;

        return score;
    }

    public int CalculateUnitTestCoverageScore(int val)
    {
        int score = 0;

        //calculate the Score
        if (val <= 92)
            score = 1;
        else if (val >= 93 && val <= 94)
            score = 2;
        else if (val == 95)
            score = 3;
        else if (val >= 96 && val <= 97)
            score = 4;
        else if (val >= 98 && val <= 100)
            score = 5;

        return score;
    }

    public int CalculateDesignReviewCoverageScore(int val)
    {
        int score = 0;

        //calculate the Score
        if (val <= 92)
            score = 1;
        else if (val >= 93 && val <= 94)
            score = 2;
        else if (val == 95)
            score = 3;
        else if (val >= 96 && val <= 97)
            score = 4;
        else if (val >= 98 && val <= 100)
            score = 5;

        return score;
    }

    public int CalculateCodeReviewCoverageScore(int val)
    {
        int score = 0;

        //calculate the Score
        if (val <= 92)
            score = 1;
        else if (val >= 93 && val <= 94)
            score = 2;
        else if (val == 95)
            score = 3;
        else if (val >= 96 && val <= 97)
            score = 4;
        else if (val >= 98 && val <= 100)
            score = 5;

        return score;
    }
}