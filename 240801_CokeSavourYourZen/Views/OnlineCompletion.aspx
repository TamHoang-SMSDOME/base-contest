<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/ClientLayout.Master" AutoEventWireup="true" CodeBehind="OnlineCompletion.aspx.cs" Inherits="BaseContest_WebForms.Views.OnlineCompletion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Headholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <img runat="server" id="ImageLink" alt="Online Submission" style="width: 100%">
    <p>
        &nbsp;
               
    </p>
    <h3 class="form-signin-heading">Online Submission</h3>

    <p class="paragraph">
        To complete the entry, please fill up the form below with your details.
               
    </p>
    <asp:HiddenField runat="server" ID="EntryID" />
    <p class="paragraph red">
        * Required 
    </p>
    <p>
        &nbsp;
               
    </p>
    <%-- <h5 style="font-weight: bold">Name *</h5>
    <asp:TextBox runat="server" ID="Name" CssClass="form-control" Text=""></asp:TextBox>
    <p>
        &nbsp;
    </p>
    <h5 style="font-weight: bold">NRIC *</h5>
    <asp:TextBox runat="server" ID="NRIC" CssClass="form-control" Text=""></asp:TextBox>
    <p>
        &nbsp;
               
    </p>--%>
    <h5 style="font-weight: bold">Receipt Number</h5>
    <asp:TextBox runat="server" ID="ReceiptNo" CssClass="form-control" Enabled="false"></asp:TextBox>
    <h5 style="font-weight: bold">Mobile Number *</h5>
    <div class="row">
               <div class="col-lg-3">
            <asp:DropDownList name="countryCode" CssClass="form-control" Enabled="false" id="Mob" ClientIDMode="Static" class="form-control" runat="server">
                <asp:ListItem data-countrycode="" value="">UK (+44)</asp:ListItem>
                <asp:ListItem data-countrycode="GB" value="44" >UK (+44)</asp:ListItem>
                <asp:ListItem data-countrycode="US" value="1">USA (+1)</asp:ListItem>
                <asp:ListItem data-countrycode="DZ" value="213">Algeria (+213)</asp:ListItem>
                <asp:ListItem data-countrycode="AD" value="376">Andorra (+376)</asp:ListItem>
                <asp:ListItem data-countrycode="AO" value="244">Angola (+244)</asp:ListItem>
                <asp:ListItem data-countrycode="AI" value="1264">Anguilla (+1264)</asp:ListItem>
                <asp:ListItem data-countrycode="AG" value="1268">Antigua &amp; Barbuda (+1268)</asp:ListItem>
                <asp:ListItem data-countrycode="AR" value="54">Argentina (+54)</asp:ListItem>
                <asp:ListItem data-countrycode="AM" value="374">Armenia (+374)</asp:ListItem>
                <asp:ListItem data-countrycode="AW" value="297">Aruba (+297)</asp:ListItem>
                <asp:ListItem data-countrycode="AU" value="61">Australia (+61)</asp:ListItem>
                <asp:ListItem data-countrycode="AT" value="43">Austria (+43)</asp:ListItem>
                <asp:ListItem data-countrycode="AZ" value="994">Azerbaijan (+994)</asp:ListItem>
                <asp:ListItem data-countrycode="BS" value="1242">Bahamas (+1242)</asp:ListItem>
                <asp:ListItem data-countrycode="BH" value="973">Bahrain (+973)</asp:ListItem>
                <asp:ListItem data-countrycode="BD" value="880">Bangladesh (+880)</asp:ListItem>
                <asp:ListItem data-countrycode="BB" value="1246">Barbados (+1246)</asp:ListItem>
                <asp:ListItem data-countrycode="BY" value="375">Belarus (+375)</asp:ListItem>
                <asp:ListItem data-countrycode="BE" value="32">Belgium (+32)</asp:ListItem>
                <asp:ListItem data-countrycode="BZ" value="501">Belize (+501)</asp:ListItem>
                <asp:ListItem data-countrycode="BJ" value="229">Benin (+229)</asp:ListItem>
                <asp:ListItem data-countrycode="BM" value="1441">Bermuda (+1441)</asp:ListItem>
                <asp:ListItem data-countrycode="BT" value="975">Bhutan (+975)</asp:ListItem>
                <asp:ListItem data-countrycode="BO" value="591">Bolivia (+591)</asp:ListItem>
                <asp:ListItem data-countrycode="BA" value="387">Bosnia Herzegovina (+387)</asp:ListItem>
                <asp:ListItem data-countrycode="BW" value="267">Botswana (+267)</asp:ListItem>
                <asp:ListItem data-countrycode="BR" value="55">Brazil (+55)</asp:ListItem>
                <asp:ListItem data-countrycode="BN" value="673">Brunei (+673)</asp:ListItem>
                <asp:ListItem data-countrycode="BG" value="359">Bulgaria (+359)</asp:ListItem>
                <asp:ListItem data-countrycode="BF" value="226">Burkina Faso (+226)</asp:ListItem>
                <asp:ListItem data-countrycode="BI" value="257">Burundi (+257)</asp:ListItem>
                <asp:ListItem data-countrycode="KH" value="855">Cambodia (+855)</asp:ListItem>
                <asp:ListItem data-countrycode="CM" value="237">Cameroon (+237)</asp:ListItem>
                <asp:ListItem data-countrycode="CA" value="1">Canada (+1)</asp:ListItem>
                <asp:ListItem data-countrycode="CV" value="238">Cape Verde Islands (+238)</asp:ListItem>
                <asp:ListItem data-countrycode="KY" value="1345">Cayman Islands (+1345)</asp:ListItem>
                <asp:ListItem data-countrycode="CF" value="236">Central African Republic (+236)</asp:ListItem>
                <asp:ListItem data-countrycode="CL" value="56">Chile (+56)</asp:ListItem>
                <asp:ListItem data-countrycode="CN" value="86">China (+86)</asp:ListItem>
                <asp:ListItem data-countrycode="CO" value="57">Colombia (+57)</asp:ListItem>
                <asp:ListItem data-countrycode="KM" value="269">Comoros (+269)</asp:ListItem>
                <asp:ListItem data-countrycode="CG" value="242">Congo (+242)</asp:ListItem>
                <asp:ListItem data-countrycode="CK" value="682">Cook Islands (+682)</asp:ListItem>
                <asp:ListItem data-countrycode="CR" value="506">Costa Rica (+506)</asp:ListItem>
                <asp:ListItem data-countrycode="HR" value="385">Croatia (+385)</asp:ListItem>
                <asp:ListItem data-countrycode="CU" value="53">Cuba (+53)</asp:ListItem>
                <asp:ListItem data-countrycode="CY" value="90392">Cyprus North (+90392)</asp:ListItem>
                <asp:ListItem data-countrycode="CY" value="357">Cyprus South (+357)</asp:ListItem>
                <asp:ListItem data-countrycode="CZ" value="42">Czech Republic (+42)</asp:ListItem>
                <asp:ListItem data-countrycode="DK" value="45">Denmark (+45)</asp:ListItem>
                <asp:ListItem data-countrycode="DJ" value="253">Djibouti (+253)</asp:ListItem>
                <asp:ListItem data-countrycode="DM" value="1809">Dominica (+1809)</asp:ListItem>
                <asp:ListItem data-countrycode="DO" value="1809">Dominican Republic (+1809)</asp:ListItem>
                <asp:ListItem data-countrycode="EC" value="593">Ecuador (+593)</asp:ListItem>
                <asp:ListItem data-countrycode="EG" value="20">Egypt (+20)</asp:ListItem>
                <asp:ListItem data-countrycode="SV" value="503">El Salvador (+503)</asp:ListItem>
                <asp:ListItem data-countrycode="GQ" value="240">Equatorial Guinea (+240)</asp:ListItem>
                <asp:ListItem data-countrycode="ER" value="291">Eritrea (+291)</asp:ListItem>
                <asp:ListItem data-countrycode="EE" value="372">Estonia (+372)</asp:ListItem>
                <asp:ListItem data-countrycode="ET" value="251">Ethiopia (+251)</asp:ListItem>
                <asp:ListItem data-countrycode="FK" value="500">Falkland Islands (+500)</asp:ListItem>
                <asp:ListItem data-countrycode="FO" value="298">Faroe Islands (+298)</asp:ListItem>
                <asp:ListItem data-countrycode="FJ" value="679">Fiji (+679)</asp:ListItem>
                <asp:ListItem data-countrycode="FI" value="358">Finland (+358)</asp:ListItem>
                <asp:ListItem data-countrycode="FR" value="33">France (+33)</asp:ListItem>
                <asp:ListItem data-countrycode="GF" value="594">French Guiana (+594)</asp:ListItem>
                <asp:ListItem data-countrycode="PF" value="689">French Polynesia (+689)</asp:ListItem>
                <asp:ListItem data-countrycode="GA" value="241">Gabon (+241)</asp:ListItem>
                <asp:ListItem data-countrycode="GM" value="220">Gambia (+220)</asp:ListItem>
                <asp:ListItem data-countrycode="GE" value="7880">Georgia (+7880)</asp:ListItem>
                <asp:ListItem data-countrycode="DE" value="49">Germany (+49)</asp:ListItem>
                <asp:ListItem data-countrycode="GH" value="233">Ghana (+233)</asp:ListItem>
                <asp:ListItem data-countrycode="GI" value="350">Gibraltar (+350)</asp:ListItem>
                <asp:ListItem data-countrycode="GR" value="30">Greece (+30)</asp:ListItem>
                <asp:ListItem data-countrycode="GL" value="299">Greenland (+299)</asp:ListItem>
                <asp:ListItem data-countrycode="GD" value="1473">Grenada (+1473)</asp:ListItem>
                <asp:ListItem data-countrycode="GP" value="590">Guadeloupe (+590)</asp:ListItem>
                <asp:ListItem data-countrycode="GU" value="671">Guam (+671)</asp:ListItem>
                <asp:ListItem data-countrycode="GT" value="502">Guatemala (+502)</asp:ListItem>
                <asp:ListItem data-countrycode="GN" value="224">Guinea (+224)</asp:ListItem>
                <asp:ListItem data-countrycode="GW" value="245">Guinea - Bissau (+245)</asp:ListItem>
                <asp:ListItem data-countrycode="GY" value="592">Guyana (+592)</asp:ListItem>
                <asp:ListItem data-countrycode="HT" value="509">Haiti (+509)</asp:ListItem>
                <asp:ListItem data-countrycode="HN" value="504">Honduras (+504)</asp:ListItem>
                <asp:ListItem data-countrycode="HK" value="852">Hong Kong (+852)</asp:ListItem>
                <asp:ListItem data-countrycode="HU" value="36">Hungary (+36)</asp:ListItem>
                <asp:ListItem data-countrycode="IS" value="354">Iceland (+354)</asp:ListItem>
                <asp:ListItem data-countrycode="IN" value="91">India (+91)</asp:ListItem>
                <asp:ListItem data-countrycode="ID" value="62">Indonesia (+62)</asp:ListItem>
                <asp:ListItem data-countrycode="IR" value="98">Iran (+98)</asp:ListItem>
                <asp:ListItem data-countrycode="IQ" value="964">Iraq (+964)</asp:ListItem>
                <asp:ListItem data-countrycode="IE" value="353">Ireland (+353)</asp:ListItem>
                <asp:ListItem data-countrycode="IL" value="972">Israel (+972)</asp:ListItem>
                <asp:ListItem data-countrycode="IT" value="39">Italy (+39)</asp:ListItem>
                <asp:ListItem data-countrycode="JM" value="1876">Jamaica (+1876)</asp:ListItem>
                <asp:ListItem data-countrycode="JP" value="81">Japan (+81)</asp:ListItem>
                <asp:ListItem data-countrycode="JO" value="962">Jordan (+962)</asp:ListItem>
                <asp:ListItem data-countrycode="KZ" value="7">Kazakhstan (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="KE" value="254">Kenya (+254)</asp:ListItem>
                <asp:ListItem data-countrycode="KI" value="686">Kiribati (+686)</asp:ListItem>
                <asp:ListItem data-countrycode="KP" value="850">Korea North (+850)</asp:ListItem>
                <asp:ListItem data-countrycode="KR" value="82">Korea South (+82)</asp:ListItem>
                <asp:ListItem data-countrycode="KW" value="965">Kuwait (+965)</asp:ListItem>
                <asp:ListItem data-countrycode="KG" value="996">Kyrgyzstan (+996)</asp:ListItem>
                <asp:ListItem data-countrycode="LA" value="856">Laos (+856)</asp:ListItem>
                <asp:ListItem data-countrycode="LV" value="371">Latvia (+371)</asp:ListItem>
                <asp:ListItem data-countrycode="LB" value="961">Lebanon (+961)</asp:ListItem>
                <asp:ListItem data-countrycode="LS" value="266">Lesotho (+266)</asp:ListItem>
                <asp:ListItem data-countrycode="LR" value="231">Liberia (+231)</asp:ListItem>
                <asp:ListItem data-countrycode="LY" value="218">Libya (+218)</asp:ListItem>
                <asp:ListItem data-countrycode="LI" value="417">Liechtenstein (+417)</asp:ListItem>
                <asp:ListItem data-countrycode="LT" value="370">Lithuania (+370)</asp:ListItem>
                <asp:ListItem data-countrycode="LU" value="352">Luxembourg (+352)</asp:ListItem>
                <asp:ListItem data-countrycode="MO" value="853">Macao (+853)</asp:ListItem>
                <asp:ListItem data-countrycode="MK" value="389">Macedonia (+389)</asp:ListItem>
                <asp:ListItem data-countrycode="MG" value="261">Madagascar (+261)</asp:ListItem>
                <asp:ListItem data-countrycode="MW" value="265">Malawi (+265)</asp:ListItem>
                <asp:ListItem data-countrycode="MY" value="60">Malaysia (+60)</asp:ListItem>
                <asp:ListItem data-countrycode="MV" value="960">Maldives (+960)</asp:ListItem>
                <asp:ListItem data-countrycode="ML" value="223">Mali (+223)</asp:ListItem>
                <asp:ListItem data-countrycode="MT" value="356">Malta (+356)</asp:ListItem>
                <asp:ListItem data-countrycode="MH" value="692">Marshall Islands (+692)</asp:ListItem>
                <asp:ListItem data-countrycode="MQ" value="596">Martinique (+596)</asp:ListItem>
                <asp:ListItem data-countrycode="MR" value="222">Mauritania (+222)</asp:ListItem>
                <asp:ListItem data-countrycode="YT" value="269">Mayotte (+269)</asp:ListItem>
                <asp:ListItem data-countrycode="MX" value="52">Mexico (+52)</asp:ListItem>
                <asp:ListItem data-countrycode="FM" value="691">Micronesia (+691)</asp:ListItem>
                <asp:ListItem data-countrycode="MD" value="373">Moldova (+373)</asp:ListItem>
                <asp:ListItem data-countrycode="MC" value="377">Monaco (+377)</asp:ListItem>
                <asp:ListItem data-countrycode="MN" value="976">Mongolia (+976)</asp:ListItem>
                <asp:ListItem data-countrycode="MS" value="1664">Montserrat (+1664)</asp:ListItem>
                <asp:ListItem data-countrycode="MA" value="212">Morocco (+212)</asp:ListItem>
                <asp:ListItem data-countrycode="MZ" value="258">Mozambique (+258)</asp:ListItem>
                <asp:ListItem data-countrycode="MN" value="95">Myanmar (+95)</asp:ListItem>
                <asp:ListItem data-countrycode="NA" value="264">Namibia (+264)</asp:ListItem>
                <asp:ListItem data-countrycode="NR" value="674">Nauru (+674)</asp:ListItem>
                <asp:ListItem data-countrycode="NP" value="977">Nepal (+977)</asp:ListItem>
                <asp:ListItem data-countrycode="NL" value="31">Netherlands (+31)</asp:ListItem>
                <asp:ListItem data-countrycode="NC" value="687">New Caledonia (+687)</asp:ListItem>
                <asp:ListItem data-countrycode="NZ" value="64">New Zealand (+64)</asp:ListItem>
                <asp:ListItem data-countrycode="NI" value="505">Nicaragua (+505)</asp:ListItem>
                <asp:ListItem data-countrycode="NE" value="227">Niger (+227)</asp:ListItem>
                <asp:ListItem data-countrycode="NG" value="234">Nigeria (+234)</asp:ListItem>
                <asp:ListItem data-countrycode="NU" value="683">Niue (+683)</asp:ListItem>
                <asp:ListItem data-countrycode="NF" value="672">Norfolk Islands (+672)</asp:ListItem>
                <asp:ListItem data-countrycode="NP" value="670">Northern Marianas (+670)</asp:ListItem>
                <asp:ListItem data-countrycode="NO" value="47">Norway (+47)</asp:ListItem>
                <asp:ListItem data-countrycode="OM" value="968">Oman (+968)</asp:ListItem>
                <asp:ListItem data-countrycode="PW" value="680">Palau (+680)</asp:ListItem>
                <asp:ListItem data-countrycode="PA" value="507">Panama (+507)</asp:ListItem>
                <asp:ListItem data-countrycode="PG" value="675">Papua New Guinea (+675)</asp:ListItem>
                <asp:ListItem data-countrycode="PY" value="595">Paraguay (+595)</asp:ListItem>
                <asp:ListItem data-countrycode="PE" value="51">Peru (+51)</asp:ListItem>
                <asp:ListItem data-countrycode="PH" value="63">Philippines (+63)</asp:ListItem>
                <asp:ListItem data-countrycode="PL" value="48">Poland (+48)</asp:ListItem>
                <asp:ListItem data-countrycode="PT" value="351">Portugal (+351)</asp:ListItem>
                <asp:ListItem data-countrycode="PR" value="1787">Puerto Rico (+1787)</asp:ListItem>
                <asp:ListItem data-countrycode="QA" value="974">Qatar (+974)</asp:ListItem>
                <asp:ListItem data-countrycode="RE" value="262">Reunion (+262)</asp:ListItem>
                <asp:ListItem data-countrycode="RO" value="40">Romania (+40)</asp:ListItem>
                <asp:ListItem data-countrycode="RU" value="7">Russia (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="RW" value="250">Rwanda (+250)</asp:ListItem>
                <asp:ListItem data-countrycode="SM" value="378">San Marino (+378)</asp:ListItem>
                <asp:ListItem data-countrycode="ST" value="239">Sao Tome &amp; Principe (+239)</asp:ListItem>
                <asp:ListItem data-countrycode="SA" value="966">Saudi Arabia (+966)</asp:ListItem>
                <asp:ListItem data-countrycode="SN" value="221">Senegal (+221)</asp:ListItem>
                <asp:ListItem data-countrycode="CS" value="381">Serbia (+381)</asp:ListItem>
                <asp:ListItem data-countrycode="SC" value="248">Seychelles (+248)</asp:ListItem>
                <asp:ListItem data-countrycode="SL" value="232">Sierra Leone (+232)</asp:ListItem>
                <asp:ListItem data-countrycode="SG" value="65" Selected>Singapore (+65)</asp:ListItem>
                <asp:ListItem data-countrycode="SK" value="421">Slovak Republic (+421)</asp:ListItem>
                <asp:ListItem data-countrycode="SI" value="386">Slovenia (+386)</asp:ListItem>
                <asp:ListItem data-countrycode="SB" value="677">Solomon Islands (+677)</asp:ListItem>
                <asp:ListItem data-countrycode="SO" value="252">Somalia (+252)</asp:ListItem>
                <asp:ListItem data-countrycode="ZA" value="27">South Africa (+27)</asp:ListItem>
                <asp:ListItem data-countrycode="ES" value="34">Spain (+34)</asp:ListItem>
                <asp:ListItem data-countrycode="LK" value="94">Sri Lanka (+94)</asp:ListItem>
                <asp:ListItem data-countrycode="SH" value="290">St. Helena (+290)</asp:ListItem>
                <asp:ListItem data-countrycode="KN" value="1869">St. Kitts (+1869)</asp:ListItem>
                <asp:ListItem data-countrycode="SC" value="1758">St. Lucia (+1758)</asp:ListItem>
                <asp:ListItem data-countrycode="SD" value="249">Sudan (+249)</asp:ListItem>
                <asp:ListItem data-countrycode="SR" value="597">Suriname (+597)</asp:ListItem>
                <asp:ListItem data-countrycode="SZ" value="268">Swaziland (+268)</asp:ListItem>
                <asp:ListItem data-countrycode="SE" value="46">Sweden (+46)</asp:ListItem>
                <asp:ListItem data-countrycode="CH" value="41">Switzerland (+41)</asp:ListItem>
                <asp:ListItem data-countrycode="SI" value="963">Syria (+963)</asp:ListItem>
                <asp:ListItem data-countrycode="TW" value="886">Taiwan (+886)</asp:ListItem>
                <asp:ListItem data-countrycode="TJ" value="7">Tajikstan (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="TH" value="66">Thailand (+66)</asp:ListItem>
                <asp:ListItem data-countrycode="TG" value="228">Togo (+228)</asp:ListItem>
                <asp:ListItem data-countrycode="TO" value="676">Tonga (+676)</asp:ListItem>
                <asp:ListItem data-countrycode="TT" value="1868">Trinidad &amp; Tobago (+1868)</asp:ListItem>
                <asp:ListItem data-countrycode="TN" value="216">Tunisia (+216)</asp:ListItem>
                <asp:ListItem data-countrycode="TR" value="90">Turkey (+90)</asp:ListItem>
                <asp:ListItem data-countrycode="TM" value="7">Turkmenistan (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="TM" value="993">Turkmenistan (+993)</asp:ListItem>
                <asp:ListItem data-countrycode="TC" value="1649">Turks &amp; Caicos Islands (+1649)</asp:ListItem>
                <asp:ListItem data-countrycode="TV" value="688">Tuvalu (+688)</asp:ListItem>
                <asp:ListItem data-countrycode="UG" value="256">Uganda (+256)</asp:ListItem>
             <%--   <asp:ListItem data-countryCode="GB" value="44">UK (+44)</asp:ListItem>--%>
                <asp:ListItem data-countrycode="UA" value="380">Ukraine (+380)</asp:ListItem>
                <asp:ListItem data-countrycode="AE" value="971">United Arab Emirates (+971)</asp:ListItem>
                <asp:ListItem data-countrycode="UY" value="598">Uruguay (+598)</asp:ListItem>
           <%--     <asp:ListItem data-countryCode="US" value="1">USA (+1)</asp:ListItem>--%>
                <asp:ListItem data-countrycode="UZ" value="7">Uzbekistan (+7)</asp:ListItem>
                <asp:ListItem data-countrycode="VU" value="678">Vanuatu (+678)</asp:ListItem>
                <asp:ListItem data-countrycode="VA" value="379">Vatican City (+379)</asp:ListItem>
                <asp:ListItem data-countrycode="VE" value="58">Venezuela (+58)</asp:ListItem>
                <asp:ListItem data-countrycode="VN" value="84">Vietnam (+84)</asp:ListItem>
                <asp:ListItem data-countrycode="VG" value="84">Virgin Islands - British (+1284)</asp:ListItem>
                <asp:ListItem data-countrycode="VI" value="84">Virgin Islands - US (+1340)</asp:ListItem>
                <asp:ListItem data-countrycode="WF" value="681">Wallis &amp; Futuna (+681)</asp:ListItem>
                <asp:ListItem data-countrycode="YE" value="969">Yemen (North)(+969)</asp:ListItem>
                <asp:ListItem data-countrycode="YE" value="967">Yemen (South)(+967)</asp:ListItem>
                <asp:ListItem data-countrycode="ZM" value="260">Zambia (+260)</asp:ListItem>
                <asp:ListItem data-countrycode="ZW" value="263">Zimbabwe (+263)</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-lg-9">
            <asp:TextBox runat="server" ID="MobileNo" CssClass="form-control" Text=""></asp:TextBox>
        </div>
    </div>

    <p>
        &nbsp;
               
    </p>
    <h5 style="font-weight: bold">Email *</h5>
    <asp:TextBox runat="server" ID="Email" CssClass="form-control" Text="" TextMode="Email"></asp:TextBox>
    <p>
        &nbsp;
               
    </p>
    <h5 style="font-weight: bold">Upload *</h5>
    <asp:FileUpload ID="Upload" runat="server" />
    <h5 style="font-weight: bold" runat="server" id="FileSpecs" visible="false"></h5>
    <%-- <asp:Button runat="server" ID="Filter"
        CssClass="btn btn-primary" Text="Upload" OnClick="Upload_Click" />--%>
    <%--  <div class="row">
        <div class="col-lg-2">
            File:
                   
        </div>
        <div class="col-lg-10">
        </div>
    </div>--%>

    <p>
        &nbsp;
               
    </p>
    <%--<h5 style="font-weight: bold">Date of Birth</h5>
    <asp:TextBox ID="DOB" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
    <p>
        &nbsp;
               
    </p>
    
    <h5 style="font-weight: bold">Receipt Number *</h5>
    <asp:TextBox runat="server" ID="ReceiptNo" CssClass="form-control" Text=""></asp:TextBox>
    <p>
        &nbsp;
               
    </p>
    <h5 style="font-weight: bold">Amount Spent *</h5>
    <asp:TextBox runat="server" ID="Amount" CssClass="form-control" Text=""></asp:TextBox>
    Please indicate amount spent on the receipt provided
    <p>
        &nbsp;
               
    </p>--%>
    <%--<label>
       <asp:CheckBox runat="server" ID="chkTnC"/>
       <span style="width:50%">
          I have read and agree to be contacted pertaining to this contest. Click here for <a href="#" target="_blank">TnCs</a>.
       </span>
   </label>
     <p>
        &nbsp;
               
    </p>--%>
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
