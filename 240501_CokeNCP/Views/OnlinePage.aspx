<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/ClientLayout.Master" AutoEventWireup="true" CodeBehind="OnlinePage.aspx.cs" Inherits="BaseContest_WebForms.Views.OnlinePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Headholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <img src="../Content/images/Coke24_SG_MayNCP_DigitalScreen_1920x400-v2.jpg" alt="Online Submission" style="width: 100%;">
    <p>
        &nbsp;
               
    </p>
    <h3 class="form-signin-heading">Online Submission</h3>


  <%--  <p class="paragraph">
        SMH / TMP / Ecom / CPET - Spend $5 on Participating Brands and stand a chance to win over $15,000 worth of prizes  <br />
        QSR / Cinemas - Upsize to a 22oz / 32oz drink from the fountain machine and stand a chance to win over $15,000 worth of prizes <br />
        FSR - Buy 2 cans or bottles of participating brands and stand a chance to win over $15,000 worth of prizes <br />
        Grand Prize: $1,500 Klook e-Vouchers x 5 Winners
            <br />
        1st Prize: Coca-Cola Luggage x 20 Winners
                        <br />
        Consolation Prizes:           
        <br />
        •&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Grab Ride $5 e-Voucher x 500 winners            
        <br />
        •&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;A pair of Golden Village Movie Tickers x 200 Winners
    </p>--%>

    <p class="paragraph">
        To participate, please fill up the form below with your details.
               
    </p>
    <p class="paragraph red">
        * Required 
    </p>
    <p>
        &nbsp;
               
    </p>
    <h5 style="font-weight: bold">Full Name (As per NRIC) *</h5>
    <asp:TextBox runat="server" ID="Name" CssClass="form-control" Text=""></asp:TextBox>
    <p>
        &nbsp;
    </p>
    <h5 style="font-weight: bold">Mobile number (SG only) *</h5>
    <div class="row">
        <div class="col-lg-3">
            <asp:DropDownList name="countryCode" CssClass="form-control" Enabled="false" ID="Mob" ClientIDMode="Static" class="form-control" runat="server">
                <asp:ListItem data-countrycode="" Value="">UK (+44)</asp:ListItem>
                <asp:ListItem data-countrycode="GB" Value="44">UK (+44)</asp:ListItem>
                <asp:ListItem data-countrycode="US" Value="1">USA (+1)</asp:ListItem>
                <asp:ListItem data-countrycode="DZ" Value="213">Algeria (+213)</asp:ListItem>
                <asp:ListItem data-countrycode="AD" Value="376">Andorra (+376)</asp:ListItem>
                <asp:ListItem data-countrycode="AO" Value="244">Angola (+244)</asp:ListItem>
                <asp:ListItem data-countrycode="AI" Value="1264">Anguilla (+1264)</asp:ListItem>
                <asp:ListItem data-countrycode="AG" Value="1268">Antigua &amp; Barbuda (+1268)</asp:ListItem>
                <asp:ListItem data-countrycode="AR" Value="54">Argentina (+54)</asp:ListItem>
                <asp:ListItem data-countrycode="AM" Value="374">Armenia (+374)</asp:ListItem>
                <asp:ListItem data-countrycode="AW" Value="297">Aruba (+297)</asp:ListItem>
                <asp:ListItem data-countrycode="AU" Value="61">Australia (+61)</asp:ListItem>
                <asp:ListItem data-countrycode="AT" Value="43">Austria (+43)</asp:ListItem>
                <asp:ListItem data-countrycode="AZ" Value="994">Azerbaijan (+994)</asp:ListItem>
                <asp:ListItem data-countrycode="BS" Value="1242">Bahamas (+1242)</asp:ListItem>
                <asp:ListItem data-countrycode="BH" Value="973">Bahrain (+973)</asp:ListItem>
                <asp:ListItem data-countrycode="BD" Value="880">Bangladesh (+880)</asp:ListItem>
                <asp:ListItem data-countrycode="BB" Value="1246">Barbados (+1246)</asp:ListItem>
                <asp:ListItem data-countrycode="BY" Value="375">Belarus (+375)</asp:ListItem>
                <asp:ListItem data-countrycode="BE" Value="32">Belgium (+32)</asp:ListItem>
                <asp:ListItem data-countrycode="BZ" Value="501">Belize (+501)</asp:ListItem>
                <asp:ListItem data-countrycode="BJ" Value="229">Benin (+229)</asp:ListItem>
                <asp:ListItem data-countrycode="BM" Value="1441">Bermuda (+1441)</asp:ListItem>
                <asp:ListItem data-countrycode="BT" Value="975">Bhutan (+975)</asp:ListItem>
                <asp:ListItem data-countrycode="BO" Value="591">Bolivia (+591)</asp:ListItem>
                <asp:ListItem data-countrycode="BA" Value="387">Bosnia Herzegovina (+387)</asp:ListItem>
                <asp:ListItem data-countrycode="BW" Value="267">Botswana (+267)</asp:ListItem>
                <asp:ListItem data-countrycode="BR" Value="55">Brazil (+55)</asp:ListItem>
                <asp:ListItem data-countrycode="BN" Value="673">Brunei (+673)</asp:ListItem>
                <asp:ListItem data-countrycode="BG" Value="359">Bulgaria (+359)</asp:ListItem>
                <asp:ListItem data-countrycode="BF" Value="226">Burkina Faso (+226)</asp:ListItem>
                <asp:ListItem data-countrycode="BI" Value="257">Burundi (+257)</asp:ListItem>
                <asp:ListItem data-countrycode="KH" Value="855">Cambodia (+855)</asp:ListItem>
                <asp:ListItem data-countrycode="CM" Value="237">Cameroon (+237)</asp:ListItem>
                <asp:ListItem data-countrycode="CA" Value="1">Canada (+1)</asp:ListItem>
                <asp:ListItem data-countrycode="CV" Value="238">Cape Verde Islands (+238)</asp:ListItem>
                <asp:ListItem data-countrycode="KY" Value="1345">Cayman Islands (+1345)</asp:ListItem>
                <asp:ListItem data-countrycode="CF" Value="236">Central African Republic (+236)</asp:ListItem>
                <asp:ListItem data-countrycode="CL" Value="56">Chile (+56)</asp:ListItem>
                <asp:ListItem data-countrycode="CN" Value="86">China (+86)</asp:ListItem>
                <asp:ListItem data-countrycode="CO" Value="57">Colombia (+57)</asp:ListItem>
                <asp:ListItem data-countrycode="KM" Value="269">Comoros (+269)</asp:ListItem>
                <asp:ListItem data-countrycode="CG" Value="242">Congo (+242)</asp:ListItem>
                <asp:ListItem data-countrycode="CK" Value="682">Cook Islands (+682)</asp:ListItem>
                <asp:ListItem data-countrycode="CR" Value="506">Costa Rica (+506)</asp:ListItem>
                <asp:ListItem data-countrycode="HR" Value="385">Croatia (+385)</asp:ListItem>
                <asp:ListItem data-countrycode="CU" Value="53">Cuba (+53)</asp:ListItem>
                <asp:ListItem data-countrycode="CY" Value="90392">Cyprus North (+90392)</asp:ListItem>
                <asp:ListItem data-countrycode="CY" Value="357">Cyprus South (+357)</asp:ListItem>
                <asp:ListItem data-countrycode="CZ" Value="42">Czech Republic (+42)</asp:ListItem>
                <asp:ListItem data-countrycode="DK" Value="45">Denmark (+45)</asp:ListItem>
                <asp:ListItem data-countrycode="DJ" Value="253">Djibouti (+253)</asp:ListItem>
                <asp:ListItem data-countrycode="DM" Value="1809">Dominica (+1809)</asp:ListItem>
                <asp:ListItem data-countrycode="DO" Value="1809">Dominican Republic (+1809)</asp:ListItem>
                <asp:ListItem data-countrycode="EC" Value="593">Ecuador (+593)</asp:ListItem>
                <asp:ListItem data-countrycode="EG" Value="20">Egypt (+20)</asp:ListItem>
                <asp:ListItem data-countrycode="SV" Value="503">El Salvador (+503)</asp:ListItem>
                <asp:ListItem data-countrycode="GQ" Value="240">Equatorial Guinea (+240)</asp:ListItem>
                <asp:ListItem data-countrycode="ER" Value="291">Eritrea (+291)</asp:ListItem>
                <asp:ListItem data-countrycode="EE" Value="372">Estonia (+372)</asp:ListItem>
                <asp:ListItem data-countrycode="ET" Value="251">Ethiopia (+251)</asp:ListItem>
                <asp:ListItem data-countrycode="FK" Value="500">Falkland Islands (+500)</asp:ListItem>
                <asp:ListItem data-countrycode="FO" Value="298">Faroe Islands (+298)</asp:ListItem>
                <asp:ListItem data-countrycode="FJ" Value="679">Fiji (+679)</asp:ListItem>
                <asp:ListItem data-countrycode="FI" Value="358">Finland (+358)</asp:ListItem>
                <asp:ListItem data-countrycode="FR" Value="33">France (+33)</asp:ListItem>
                <asp:ListItem data-countrycode="GF" Value="594">French Guiana (+594)</asp:ListItem>
                <asp:ListItem data-countrycode="PF" Value="689">French Polynesia (+689)</asp:ListItem>
                <asp:ListItem data-countrycode="GA" Value="241">Gabon (+241)</asp:ListItem>
                <asp:ListItem data-countrycode="GM" Value="220">Gambia (+220)</asp:ListItem>
                <asp:ListItem data-countrycode="GE" Value="7880">Georgia (+7880)</asp:ListItem>
                <asp:ListItem data-countrycode="DE" Value="49">Germany (+49)</asp:ListItem>
                <asp:ListItem data-countrycode="GH" Value="233">Ghana (+233)</asp:ListItem>
                <asp:ListItem data-countrycode="GI" Value="350">Gibraltar (+350)</asp:ListItem>
                <asp:ListItem data-countrycode="GR" Value="30">Greece (+30)</asp:ListItem>
                <asp:ListItem data-countrycode="GL" Value="299">Greenland (+299)</asp:ListItem>
                <asp:ListItem data-countrycode="GD" Value="1473">Grenada (+1473)</asp:ListItem>
                <asp:ListItem data-countrycode="GP" Value="590">Guadeloupe (+590)</asp:ListItem>
                <asp:ListItem data-countrycode="GU" Value="671">Guam (+671)</asp:ListItem>
                <asp:ListItem data-countrycode="GT" Value="502">Guatemala (+502)</asp:ListItem>
                <asp:ListItem data-countrycode="GN" Value="224">Guinea (+224)</asp:ListItem>
                <asp:ListItem data-countrycode="GW" Value="245">Guinea - Bissau (+245)</asp:ListItem>
                <asp:ListItem data-countrycode="GY" Value="592">Guyana (+592)</asp:ListItem>
                <asp:ListItem data-countrycode="HT" Value="509">Haiti (+509)</asp:ListItem>
                <asp:ListItem data-countrycode="HN" Value="504">Honduras (+504)</asp:ListItem>
                <asp:ListItem data-countrycode="HK" Value="852">Hong Kong (+852)</asp:ListItem>
                <asp:ListItem data-countrycode="HU" Value="36">Hungary (+36)</asp:ListItem>
                <asp:ListItem data-countrycode="IS" Value="354">Iceland (+354)</asp:ListItem>
                <asp:ListItem data-countrycode="IN" Value="91">India (+91)</asp:ListItem>
                <asp:ListItem data-countrycode="ID" Value="62">Indonesia (+62)</asp:ListItem>
                <asp:ListItem data-countrycode="IR" Value="98">Iran (+98)</asp:ListItem>
                <asp:ListItem data-countrycode="IQ" Value="964">Iraq (+964)</asp:ListItem>
                <asp:ListItem data-countrycode="IE" Value="353">Ireland (+353)</asp:ListItem>
                <asp:ListItem data-countrycode="IL" Value="972">Israel (+972)</asp:ListItem>
                <asp:ListItem data-countrycode="IT" Value="39">Italy (+39)</asp:ListItem>
                <asp:ListItem data-countrycode="JM" Value="1876">Jamaica (+1876)</asp:ListItem>
                <asp:ListItem data-countrycode="JP" Value="81">Japan (+81)</asp:ListItem>
                <asp:ListItem data-countrycode="JO" Value="962">Jordan (+962)</asp:ListItem>
                <asp:ListItem data-countrycode="KZ" Value="7">Kazakhstan (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="KE" Value="254">Kenya (+254)</asp:ListItem>
                <asp:ListItem data-countrycode="KI" Value="686">Kiribati (+686)</asp:ListItem>
                <asp:ListItem data-countrycode="KP" Value="850">Korea North (+850)</asp:ListItem>
                <asp:ListItem data-countrycode="KR" Value="82">Korea South (+82)</asp:ListItem>
                <asp:ListItem data-countrycode="KW" Value="965">Kuwait (+965)</asp:ListItem>
                <asp:ListItem data-countrycode="KG" Value="996">Kyrgyzstan (+996)</asp:ListItem>
                <asp:ListItem data-countrycode="LA" Value="856">Laos (+856)</asp:ListItem>
                <asp:ListItem data-countrycode="LV" Value="371">Latvia (+371)</asp:ListItem>
                <asp:ListItem data-countrycode="LB" Value="961">Lebanon (+961)</asp:ListItem>
                <asp:ListItem data-countrycode="LS" Value="266">Lesotho (+266)</asp:ListItem>
                <asp:ListItem data-countrycode="LR" Value="231">Liberia (+231)</asp:ListItem>
                <asp:ListItem data-countrycode="LY" Value="218">Libya (+218)</asp:ListItem>
                <asp:ListItem data-countrycode="LI" Value="417">Liechtenstein (+417)</asp:ListItem>
                <asp:ListItem data-countrycode="LT" Value="370">Lithuania (+370)</asp:ListItem>
                <asp:ListItem data-countrycode="LU" Value="352">Luxembourg (+352)</asp:ListItem>
                <asp:ListItem data-countrycode="MO" Value="853">Macao (+853)</asp:ListItem>
                <asp:ListItem data-countrycode="MK" Value="389">Macedonia (+389)</asp:ListItem>
                <asp:ListItem data-countrycode="MG" Value="261">Madagascar (+261)</asp:ListItem>
                <asp:ListItem data-countrycode="MW" Value="265">Malawi (+265)</asp:ListItem>
                <asp:ListItem data-countrycode="MY" Value="60">Malaysia (+60)</asp:ListItem>
                <asp:ListItem data-countrycode="MV" Value="960">Maldives (+960)</asp:ListItem>
                <asp:ListItem data-countrycode="ML" Value="223">Mali (+223)</asp:ListItem>
                <asp:ListItem data-countrycode="MT" Value="356">Malta (+356)</asp:ListItem>
                <asp:ListItem data-countrycode="MH" Value="692">Marshall Islands (+692)</asp:ListItem>
                <asp:ListItem data-countrycode="MQ" Value="596">Martinique (+596)</asp:ListItem>
                <asp:ListItem data-countrycode="MR" Value="222">Mauritania (+222)</asp:ListItem>
                <asp:ListItem data-countrycode="YT" Value="269">Mayotte (+269)</asp:ListItem>
                <asp:ListItem data-countrycode="MX" Value="52">Mexico (+52)</asp:ListItem>
                <asp:ListItem data-countrycode="FM" Value="691">Micronesia (+691)</asp:ListItem>
                <asp:ListItem data-countrycode="MD" Value="373">Moldova (+373)</asp:ListItem>
                <asp:ListItem data-countrycode="MC" Value="377">Monaco (+377)</asp:ListItem>
                <asp:ListItem data-countrycode="MN" Value="976">Mongolia (+976)</asp:ListItem>
                <asp:ListItem data-countrycode="MS" Value="1664">Montserrat (+1664)</asp:ListItem>
                <asp:ListItem data-countrycode="MA" Value="212">Morocco (+212)</asp:ListItem>
                <asp:ListItem data-countrycode="MZ" Value="258">Mozambique (+258)</asp:ListItem>
                <asp:ListItem data-countrycode="MN" Value="95">Myanmar (+95)</asp:ListItem>
                <asp:ListItem data-countrycode="NA" Value="264">Namibia (+264)</asp:ListItem>
                <asp:ListItem data-countrycode="NR" Value="674">Nauru (+674)</asp:ListItem>
                <asp:ListItem data-countrycode="NP" Value="977">Nepal (+977)</asp:ListItem>
                <asp:ListItem data-countrycode="NL" Value="31">Netherlands (+31)</asp:ListItem>
                <asp:ListItem data-countrycode="NC" Value="687">New Caledonia (+687)</asp:ListItem>
                <asp:ListItem data-countrycode="NZ" Value="64">New Zealand (+64)</asp:ListItem>
                <asp:ListItem data-countrycode="NI" Value="505">Nicaragua (+505)</asp:ListItem>
                <asp:ListItem data-countrycode="NE" Value="227">Niger (+227)</asp:ListItem>
                <asp:ListItem data-countrycode="NG" Value="234">Nigeria (+234)</asp:ListItem>
                <asp:ListItem data-countrycode="NU" Value="683">Niue (+683)</asp:ListItem>
                <asp:ListItem data-countrycode="NF" Value="672">Norfolk Islands (+672)</asp:ListItem>
                <asp:ListItem data-countrycode="NP" Value="670">Northern Marianas (+670)</asp:ListItem>
                <asp:ListItem data-countrycode="NO" Value="47">Norway (+47)</asp:ListItem>
                <asp:ListItem data-countrycode="OM" Value="968">Oman (+968)</asp:ListItem>
                <asp:ListItem data-countrycode="PW" Value="680">Palau (+680)</asp:ListItem>
                <asp:ListItem data-countrycode="PA" Value="507">Panama (+507)</asp:ListItem>
                <asp:ListItem data-countrycode="PG" Value="675">Papua New Guinea (+675)</asp:ListItem>
                <asp:ListItem data-countrycode="PY" Value="595">Paraguay (+595)</asp:ListItem>
                <asp:ListItem data-countrycode="PE" Value="51">Peru (+51)</asp:ListItem>
                <asp:ListItem data-countrycode="PH" Value="63">Philippines (+63)</asp:ListItem>
                <asp:ListItem data-countrycode="PL" Value="48">Poland (+48)</asp:ListItem>
                <asp:ListItem data-countrycode="PT" Value="351">Portugal (+351)</asp:ListItem>
                <asp:ListItem data-countrycode="PR" Value="1787">Puerto Rico (+1787)</asp:ListItem>
                <asp:ListItem data-countrycode="QA" Value="974">Qatar (+974)</asp:ListItem>
                <asp:ListItem data-countrycode="RE" Value="262">Reunion (+262)</asp:ListItem>
                <asp:ListItem data-countrycode="RO" Value="40">Romania (+40)</asp:ListItem>
                <asp:ListItem data-countrycode="RU" Value="7">Russia (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="RW" Value="250">Rwanda (+250)</asp:ListItem>
                <asp:ListItem data-countrycode="SM" Value="378">San Marino (+378)</asp:ListItem>
                <asp:ListItem data-countrycode="ST" Value="239">Sao Tome &amp; Principe (+239)</asp:ListItem>
                <asp:ListItem data-countrycode="SA" Value="966">Saudi Arabia (+966)</asp:ListItem>
                <asp:ListItem data-countrycode="SN" Value="221">Senegal (+221)</asp:ListItem>
                <asp:ListItem data-countrycode="CS" Value="381">Serbia (+381)</asp:ListItem>
                <asp:ListItem data-countrycode="SC" Value="248">Seychelles (+248)</asp:ListItem>
                <asp:ListItem data-countrycode="SL" Value="232">Sierra Leone (+232)</asp:ListItem>
                <asp:ListItem data-countrycode="SG" Value="65" Selected>Singapore (+65)</asp:ListItem>
                <asp:ListItem data-countrycode="SK" Value="421">Slovak Republic (+421)</asp:ListItem>
                <asp:ListItem data-countrycode="SI" Value="386">Slovenia (+386)</asp:ListItem>
                <asp:ListItem data-countrycode="SB" Value="677">Solomon Islands (+677)</asp:ListItem>
                <asp:ListItem data-countrycode="SO" Value="252">Somalia (+252)</asp:ListItem>
                <asp:ListItem data-countrycode="ZA" Value="27">South Africa (+27)</asp:ListItem>
                <asp:ListItem data-countrycode="ES" Value="34">Spain (+34)</asp:ListItem>
                <asp:ListItem data-countrycode="LK" Value="94">Sri Lanka (+94)</asp:ListItem>
                <asp:ListItem data-countrycode="SH" Value="290">St. Helena (+290)</asp:ListItem>
                <asp:ListItem data-countrycode="KN" Value="1869">St. Kitts (+1869)</asp:ListItem>
                <asp:ListItem data-countrycode="SC" Value="1758">St. Lucia (+1758)</asp:ListItem>
                <asp:ListItem data-countrycode="SD" Value="249">Sudan (+249)</asp:ListItem>
                <asp:ListItem data-countrycode="SR" Value="597">Suriname (+597)</asp:ListItem>
                <asp:ListItem data-countrycode="SZ" Value="268">Swaziland (+268)</asp:ListItem>
                <asp:ListItem data-countrycode="SE" Value="46">Sweden (+46)</asp:ListItem>
                <asp:ListItem data-countrycode="CH" Value="41">Switzerland (+41)</asp:ListItem>
                <asp:ListItem data-countrycode="SI" Value="963">Syria (+963)</asp:ListItem>
                <asp:ListItem data-countrycode="TW" Value="886">Taiwan (+886)</asp:ListItem>
                <asp:ListItem data-countrycode="TJ" Value="7">Tajikstan (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="TH" Value="66">Thailand (+66)</asp:ListItem>
                <asp:ListItem data-countrycode="TG" Value="228">Togo (+228)</asp:ListItem>
                <asp:ListItem data-countrycode="TO" Value="676">Tonga (+676)</asp:ListItem>
                <asp:ListItem data-countrycode="TT" Value="1868">Trinidad &amp; Tobago (+1868)</asp:ListItem>
                <asp:ListItem data-countrycode="TN" Value="216">Tunisia (+216)</asp:ListItem>
                <asp:ListItem data-countrycode="TR" Value="90">Turkey (+90)</asp:ListItem>
                <asp:ListItem data-countrycode="TM" Value="7">Turkmenistan (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="TM" Value="993">Turkmenistan (+993)</asp:ListItem>
                <asp:ListItem data-countrycode="TC" Value="1649">Turks &amp; Caicos Islands (+1649)</asp:ListItem>
                <asp:ListItem data-countrycode="TV" Value="688">Tuvalu (+688)</asp:ListItem>
                <asp:ListItem data-countrycode="UG" Value="256">Uganda (+256)</asp:ListItem>
                <%--   <asp:ListItem data-countryCode="GB" value="44">UK (+44)</asp:ListItem>--%>
                <asp:ListItem data-countrycode="UA" Value="380">Ukraine (+380)</asp:ListItem>
                <asp:ListItem data-countrycode="AE" Value="971">United Arab Emirates (+971)</asp:ListItem>
                <asp:ListItem data-countrycode="UY" Value="598">Uruguay (+598)</asp:ListItem>
                <%--     <asp:ListItem data-countryCode="US" value="1">USA (+1)</asp:ListItem>--%>
                <asp:ListItem data-countrycode="UZ" Value="7">Uzbekistan (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="VU" Value="678">Vanuatu (+678)</asp:ListItem>
                <asp:ListItem data-countrycode="VA" Value="379">Vatican City (+379)</asp:ListItem>
                <asp:ListItem data-countrycode="VE" Value="58">Venezuela (+58)</asp:ListItem>
                <asp:ListItem data-countrycode="VN" Value="84">Vietnam (+84)</asp:ListItem>
                <asp:ListItem data-countrycode="VG" Value="84">Virgin Islands - British (+1284)</asp:ListItem>
                <asp:ListItem data-countrycode="VI" Value="84">Virgin Islands - US (+1340)</asp:ListItem>
                <asp:ListItem data-countrycode="WF" Value="681">Wallis &amp; Futuna (+681)</asp:ListItem>
                <asp:ListItem data-countrycode="YE" Value="969">Yemen (North)(+969)</asp:ListItem>
                <asp:ListItem data-countrycode="YE" Value="967">Yemen (South)(+967)</asp:ListItem>
                <asp:ListItem data-countrycode="ZM" Value="260">Zambia (+260)</asp:ListItem>
                <asp:ListItem data-countrycode="ZW" Value="263">Zimbabwe (+263)</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-lg-9">
            <asp:TextBox runat="server" ID="MobileNo" CssClass="form-control" Text="" MaxLength="8"></asp:TextBox>
        </div>
    </div>
    <p>
        &nbsp;
               
    </p>

    <h5 style="font-weight: bold">Email Address *</h5>
    <asp:TextBox runat="server" ID="Email" CssClass="form-control" Text="" TextMode="Email"></asp:TextBox>
    <p>
        &nbsp;
    </p>
    <h5 style="font-weight: bold">Retailer *</h5>
    <asp:DropDownList ID="Retailer" CssClass="form-control" runat="server" />
    <p>
        &nbsp;         
    </p>

    <h5 style="font-weight: bold">Amount Spent (Please indicate amount spent on Participating Products Only) *</h5>
    <asp:TextBox runat="server" ID="Amount" CssClass="form-control" Text=""></asp:TextBox>
    Please indicate amount spent on the receipt provided
    <p>
        &nbsp;
               
    </p>
    <h5 style="font-weight: bold">Receipt Number *</h5>
    <asp:TextBox runat="server" ID="ReceiptNo" CssClass="form-control" Text=""></asp:TextBox>
    <p>
        &nbsp;
               
    </p>
    <h5 style="font-weight: bold">Upload of receipt image *</h5>
    <asp:FileUpload ID="Upload" runat="server" />
    <h5 style="font-weight: bold" runat="server" id="FileSpecs" visible="false"></h5>
    <p>
        &nbsp;
               
    </p>
    <label>
        <asp:CheckBox runat="server" ID="chkTnC" />
        <span style="width: 50%">I confirm that the information submitted is true and accurate. In the event the information submitted is found to be fraudulent, erroneous and/or incomplete in any way, I acknowledge that my participation in the Promotion may be rejected or deemed void at the Company’s sole discretion.</span>
    </label>
    <p>
        &nbsp;
               
    </p>
    <style>
        .term-and-cond-link {
            pointer-events: none;
            cursor: default;
            text-decoration: none;
        }
    </style>
    <label>
        <asp:CheckBox runat="server" ID="chkTnC2" />
        <span style="width: 50%">I acknowledge that I have read and understood the 
           <asp:HyperLink runat="server" NavigateUrl="~/Content/TC - Travel Breaks (1st May - 31st May 2024) V2.pdf" Target="_blank" Text="Terms & Conditions" />
            and the 
           <asp:HyperLink runat="server" NavigateUrl="~/Content/TnC.pdf?t=20220224_105301" Target="_blank" Text="Privacy & Cookie Policy" CssClass="term-and-cond-link" />, and consent to the collection, use and disclosure of my personal data by Coca-Cola Singapore for the purposes set out therein.</span>
    </label>
    <p>
        &nbsp;
               
    </p>
    <asp:Button runat="server" ID="Submit"
        CssClass="btn btn-primary" Text="Submit" OnClick="Submit_Click" />

    <!-- Modal -->
    <div id="divPopUp" class="modal fade" aria-hidden="false" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <%--    <div class="modal-header">
                                <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
                                <h4 class="modal-title"></h4>
                            </div>--%>
                <div class="modal-body">
                    <h3>
                        <asp:Label runat="server" ID="lblModal" Text=""> </asp:Label></h3>
                </div>
                <div class="modal-footer">
                    <button type="button" runat="server" id="btnCancel" class="btn btn-primary" data-bs-dismiss="modal">Ok</button>
                    <button type="button" runat="server" id="btnOk" class="btn btn-primary" data-bs-dismiss="modal" onclick="redirectUrl()">Ok</button>
                    <%--   <asp:Button ID="btnCancel" runat="server" Text=""  OnClientClick="" />--%>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FootHolder" runat="server">
    <script>
        $(document).ready(function () {
            $('#Main_Retailer').select2({
                width: '100%'
            });
        });
        $(document).ready(function () {
            jQuery('.datetimepicker').datetimepicker();
        });

        function redirectUrl() {
            var url = window.location.href
            location.replace(url)
        }
    </script>
</asp:Content>
