<?php
include_once('./_common.php');
include_once('../head.php');


$is_day = ($dl) ? true : false;
$today = getdate();
$b_mon = $today['mon'];
$b_day = $today['mday'];
$b_year = $today['year'];
if($year < 1) { // 오늘의 달력 일때
  $month = $b_mon;
  $mday = $b_day;
  $year = $b_year;
}
if(!$day) $day = $b_day;
if(isset($_GET['month'])) {
	$month = $_GET['month'];
}
$lastday=array(0,31,28,31,30,31,30,31,31,30,31,30,31);
if($year%4 == 0) $lastday[2] = 29;
$dayoftheweek = date("w", mktime(0,0,0,$month,1,$year));

$sca_qstr = ($sca) ? '&amp;sca='.urlencode($sca) : '';

if($month == 1) { 
	$year_prev = $year - 1;
	$month_prev = 12;
	$year_next = $year;
	$month_next = $month + 1;
} else if($month == 12) { 
	$year_prev = $year; 
	$month_prev = $month - 1;
	$year_next = $year + 1;
	$month_next = 1;
} else {
	$year_prev = $year; 
	$month_prev = $month - 1;
	$year_next = $year;
	$month_next = $month + 1;
}
// 날짜
$nowday = $today['year'].sprintf("%02d",$today['mon']).sprintf("%02d",$today['mday']);
$selday = $year.sprintf("%02d",$month).sprintf("%02d",$day);


?>
<script>
var editIndex = undefined;
var reservationHeader = [
    [{
            field: 'ck',
            checkbox: true
        },
        {
            field: 'rsv_idx',
            title: 'rsv_idx',
            width: 50,
            hidden: true
        },
        {
            field: 'stf_name',
            title: '테라피스트',
            width: 150
        },
        {
            field: 'c_name',
            title: '코스명',
            width: 150
        },
        {
            field: 'c_price',
            title: '가격',
            width: 150
        },
        {
            field: 'rsv_date',
            title: '예약일',
            width: 150
        },
        {
            field: 'st_name',
            title: '예약시간',
            width: 150
        },
        {
            field: 'mb_name',
            title: '고객명',
            width: 150
        },
        {
            field: 'mb_hp',
            title: '고객연락처',
            width: 150
        },
        {
            field: 'rsv_datetime',
            title: '에약날짜',
            width: 200
        }
    ]
];

$(document).ready(function() {
    // 앤터키
    $("input").keydown(function(key) {
        if (key.keyCode == 13) {
            $('#search_btn').trigger('click');
        }
    });

    // 그리드 초기화
    $('#reservationList').datagrid({
        columns: reservationHeader,
        url: '<?php echo BT_AJAX_URL;?>/ajax.reservationList.php?action=select',
        height: $(window).height() - 280,
        pageSize: 50
    });

    $('#month').on('change', function() {
        location.href = "http://admin.s2bt.com/reservation/reservationCal.php?month=" + this.value;
    });
    $('#todaymonth').text('<?php echo $month ?>월');
    $('#month').var(<?php echo $month ?>);
});


function endEditing() {
    if (editIndex == undefined) {
        return true
    }
    if ($('#reservationList').datagrid('validateRow', editIndex)) {
        $('#reservationList').datagrid('endEdit', editIndex);
        editIndex = undefined;
        return true;
    } else {
        return false;
    }
}

function onClickCell(index, field) {
    if (endEditing()) {
        $('#reservationList').datagrid('selectRow', index)
            .datagrid('beginEdit', index);
        var ed = $('#reservationList').datagrid('getEditor', {
            index: index,
            field: field
        });
        if (ed) {
            ($(ed.target).data('textbox') ? $(ed.target).textbox('textbox') : $(ed.target)).focus();
        }
        editIndex = index;
    } else {
        setTimeout(function() {
            $('#reservationList').datagrid('selectRow', editIndex);
        }, 0);
    }
}

function pad(n, width) {
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join('0') + n;
}

function getList(y, m, d) {
    $('#reservationList').datagrid('load', {
        rsv_date: y + "-" + pad(m, 2) + "-" + pad(d, 2)
    });
}
// 그리드 검색
function search() {
    $('#reservationList').datagrid('load', {
        ppc1_name: $('input[name="ppc1_name"]').val()
    });
}

function onEndEdit(index, row) {
    var ed = $(this).datagrid('getEditor', {
        index: index,
        field: 'ppc1_idx'
    });
}

function getRowIndex(target) {
    var tr = $(target).closest('tr.datagrid-row');
    return parseInt(tr.attr('datagrid-row-index'));
}

function insertRow() {
    if (endEditing()) {
        $('#reservationList').datagrid('appendRow', {});
        editIndex = $('#reservationList').datagrid('getRows').length - 1;
        $('#reservationList').datagrid('selectRow', editIndex)
            .datagrid('beginEdit', editIndex);
    }
}

function updateRow() {
    $('#reservationList').datagrid('endEdit', editIndex);
    var rows = $('#reservationList').datagrid('getChanges');
    $.ajax({
        type: "POST",
        url: "<?php echo BT_AJAX_URL; ?>/ajax.reservationList.php?action=update",
        data: {
            rows: rows
        },
        dataType: "json",
        success: function(data) {
            $('#reservationList').datagrid('reload');
        },
        error: function(xhr, status, error) {
            $('#reservationList').datagrid('reload');
            alert(error);
        }
    });
}

function deleteRow() {
    if ($('#reservationList').datagrid('getSelections').length <= 0) {
        alert("항목을 선택해주세요");
        return;
    }
    $.messager.confirm('Confirm', '선택 항목을 삭제 하시겠습니까?', function(r) {
        if (r) {
            var rows = $('#reservationList').datagrid('getSelections');
            for (var j = 0; j < rows.length; j++) {
                var index = $('#reservationList').datagrid('getRowIndex', rows[j]);
                $('#reservationList').datagrid('deleteRow', index);
            }
            $.ajax({
                type: "POST",
                url: "<?php echo BT_AJAX_URL; ?>/ajax.reservationList.php?action=delete",
                data: {
                    rows: rows
                },
                dataType: "json",
                success: function(data) {
                    //alert(data.records+'행 삭제');
                    $('#reservationList').datagrid('reload');
                },
                error: function(xhr, status, error) {
                    alert(error);
                }
            });
        }
    });
}

function reject() {
    $('#reservationList').datagrid('rejectChanges');
    editIndex = undefined;
}
</script>
<style>
.en {
    font-size: 16px;
    font-weight: bold;
}

.hidden-xs {
    color: black;
}
</style>
<div class="row">
    <section class="col-lg-12 connectedSortable">
        <div class="card">
            <div class="tab-content no-padding">
                <div class="" style="width: 600px">
                    <div class="" id="todaymonth" style="font-size: 25px; text-align: center;">
                    </div>
                    <select name="month" id="month">
                        <?php for($i=1;$i<=12;$i++) {?>
                        <option value="<?php echo $i;?>"
                            <?php if($_GET['month'] == $i || $month == $i) echo "selected" ?>>
                            <?php echo $i;?>월
                        </option>
                        <?php }?>
                    </select>
                    <table class="table table-bordered list-tbl en font-12 "
                        style="<?php if(!$G5_IS_MOBILE) echo 'table-layout: fixed';?>; width:600px;">
                        <tr class="active">
                            <th class="red"><span class="hidden-xs">일</span></th>
                            <th><span class="hidden-xs">월</span></th>
                            <th><span class="hidden-xs">화</span></th>
                            <th><span class="hidden-xs">수</span></th>
                            <th><span class="hidden-xs">목</span></th>
                            <th><span class="hidden-xs">금</span></th>
                            <th class="blue"><span class="hidden-xs">토</span></th>
                        </tr>
                        <?php
						$cnday = array();
						$myday = array();
						$cday = 1;
						$sel_mon = sprintf("%02d",$month);
						$now_month = $year.$sel_mon;
						$sca_sql = ($sca) ? "and ca_name = '".$sca."'" : "";
						//$result = sql_query("select * from $write_table where wr_is_comment = '0' and left(wr_2,6) = '{$now_month}' $sca_sql order by wr_id asc");
						
						/*while ($row = sql_fetch_array($result)) {

							$sday = (substr($row['wr_2'],0,6) <  $now_month) ? 1 : substr($row['wr_2'],6,2);
							$sday= (int)$sday;
							if(!$cnday[$sday]) $cnday[$sday] = 0;
							$cnday[$sday]++;
						}*/
						$temp = 7 - (($lastday[$month]+$dayoftheweek)%7);

						if($temp == 7) $temp = 0;
							
						$lastcount = $lastday[$month]+$dayoftheweek + $temp;

						for ($iz = 1; $iz <= $lastcount; $iz++) {

							if($day) {
								$is_today = ($day == $cday) ? true : false;
							} else {
								$is_today = ($b_year == $year && $b_mon == $month && $b_day == $cday) ? true : false;
							}

							$daytext = ($is_today) ? '<b>'.$cday.'</b>' : $cday;

							$daycolor = '';
							if($iz%7 == 1) {
								echo '<tr>'.PHP_EOL;
								$daycolor = ' red';
							} else if($iz%7 == 0) {
								$daycolor = ' blue';
							} 

							if($dayoftheweek < $iz && $iz <= $lastday[$month]+$dayoftheweek) {
							?>
                        <td class="<?php echo $daycolor;?> td-content <?php echo ($is_today) ? ' active' : '';?>" style="
                            <?php if($G5_IS_MOBILE) { echo 'height: 55px;'; }?>">
                            <a href="#"
                                onClick="getList(<?php echo $year;?>, <?php echo $month;?>, <?php echo $cday;?>)">
                                <span class="en"
                                    style="<?php if($daycolor == ' red' || $daycolor == ' blue') echo 'color: black;'; ?>">
                                    <?php echo $daytext;?><br>
                                </span>
                                <?php if($cnday[$cday]) { ?>
                                <span class=" badge bg-blue font-10 en pull-right"
                                    style="margin-top:2px;"><?php echo number_format($cnday[$cday]);?></span>
                                <?php } ?>
                            </a>
                            <?php 
										$cnt = count($list);
										$dayk++;
										$mm=sprintf("%02d", $month);						//sprintf함수와 %0d 를 함께사용하면 여백에 0을 채워넣을 수 있다
										$ss=sprintf("%02d", $dayk);
										$ql = "SELECT *  FROM  `s2bt_reservation` AS A INNER JOIN `g5_member` AS B ON A.mb_num = B.mb_num WHERE `rsv_date` LIKE '$year-$mm-$ss'";
										$result2 =  sql_query($ql);
										$rows = array();
                                        $row = sql_fetch_array($result2);
										while ($row = sql_fetch_array($result2)) {
											array_push($rows, array(
												"mb_num"=>$row['mb_num'],
												"mb_id"=>$row['mb_id'],
												"mb_name"=>$row['mb_name'],
												"mb_hp"=>$row['mb_hp'],
												"rsv_idx"=>$row['rsv_idx']
											));
										}
                                        $reservationListMembers = array();
										for($i=0;$i<count($rows);$i++) {
                                            array_push($reservationListMembers,$rows[$i]['mb_name']);
                                            $reservationListMembers = array_unique($reservationListMembers);
											//echo '<a href="'.BT_URL.'/customer/coustomerPoint.php?mb_id='.$rows[$i]['mb_id'].'" target="_blank">'.$rows[$i]['mb_name'].'</a></br>';
                                            //echo json_encode($rows[$i]);
										}
                                        for($i=0;$i<count($reservationListMembers);$i++)
                                        {
                                            echo "<a>".$reservationListMembers[$i]."</a><br/>";
                                        }
									?>
                            <?php
									//for문을 돌려서  해당 카테고리의 글 숫자를 출력해준다.
									for($ji = 0;$ji<count($categories);$ji++){
										if($jiyuk[$ji]>0 && ($sca ==NULL || $sca == $categories[$ji])){?>
                            <a href="#"
                                onClick="getList(<?php echo $year;?>, <?php echo $month;?>, <?php echo $cday;?>)">
                                <?php echo $categories[$ji];
												echo ':';
												echo $jiyuk[$ji];
												echo '<br>';
										}?>
                                <?php }?>
                            </a>
                        </td>
                        <?php
							
								$cday++;
							} else { 
								echo '<td></td>'.PHP_EOL; 
							}
						
							if($iz%7 == 0) echo '</tr>'.PHP_EOL;
						}
					?>
                    </table>
                </div>
                <div class="chart tab-pane active" id="revenue-chart" style="position: relative; padding:20px;">
                    <form class="form-inline" style="margin-bottom:30px;">
                        <div class="form-group f-search">
                            <label for="mb_num" style="margin-right:10px;">고객번호</label>
                            <input type="text" class="form-control" name="mb_num" id="mb_num" placeholder="고객번호">
                        </div>

                        <div id="search_btn" class="btn btn-primary" onClick="search()">검색</div>
                    </form>

                    <table id="reservationList" class="easyui-datagrid" style="width:100%;margin-bottom:20px;"
                        pagination="true"
                        data-options="rownumbers:true, singleSelect:true,collapsible:true,url:'<?php echo BT_AJAX_URL.'/ajax.reservationList.php?action=select';?>', method:'post', onClickCell: onClickCell, onEndEdit: onEndEdit">
                    </table>
                    <!--<div id="reservationListTb" style="height:auto">
					<a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="insertRow()">추가</a>
					<a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true" onclick="updateRow()">저장</a>
					<a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="deleteRow()">삭제</a>
					<a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-undo',plain:true" onclick="reject()">취소</a>
				</div>-->
                </div>
            </div>
        </div>
    </section>
</div>
<?php
include_once('../tail.php');
?>