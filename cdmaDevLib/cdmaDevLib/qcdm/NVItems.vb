Imports System.Text

Public Class NvItems
    Public Enum NVItems As Long
        ''Random sht for make fix decoder? wat?
        NOT_AN_NV_ITEM = -1222

        NV_ESN_I = 0
        NV_ESN_CHKSUM_I
        NV_VERNO_MAJ_I
        NV_VERNO_MIN_I
        NV_SCM_I
        NV_SLOT_CYCLE_INDEX_I
        NV_MOB_CAI_REV_I
        NV_MOB_FIRM_REV_I
        NV_MOB_MODEL_I
        NV_CONFIG_CHKSUM_I
        NV_PREF_MODE_I
        NV_CDMA_PREF_SERV_I
        NV_ANALOG_PREF_SERV_I
        NV_CDMA_SID_LOCK_I
        NV_CDMA_SID_ACQ_I
        NV_ANALOG_SID_LOCK_I
        NV_ANALOG_SID_ACQ_I
        NV_ANALOG_FIRSTCHP_I
        NV_ANALOG_HOME_SID_I
        NV_ANALOG_REG_I
        NV_PCDMACH_I
        NV_SCDMACH_I
        NV_PPCNCH_I
        NV_SPCNCH_I
        NV_NAM_CHKSUM_I
        NV_A_KEY_I
        NV_A_KEY_CHKSUM_I
        NV_SSD_A_I
        NV_SSD_A_CHKSUM_I
        NV_SSD_B_I
        NV_SSD_B_CHKSUM_I
        NV_COUNT_I
        NV_MIN1_I
        NV_MIN2_I
        NV_MOB_TERM_HOME_I
        NV_MOB_TERM_FOR_SID_I
        NV_MOB_TERM_FOR_NID_I
        NV_ACCOLC_I
        NV_SID_NID_I
        NV_MIN_CHKSUM_I
        NV_CURR_NAM_I
        NV_ORIG_MIN_I
        NV_AUTO_NAM_I
        NV_NAME_NAM_I
        NV_NXTREG_I
        NV_LSTSID_I
        NV_LOCAID_I
        NV_PUREG_I
        NV_ZONE_LIST_I
        NV_SID_NID_LIST_I
        NV_DIST_REG_I
        NV_LAST_CDMACH_I
        NV_CALL_TIMER_I
        NV_AIR_TIMER_I
        NV_ROAM_TIMER_I
        NV_LIFE_TIMER_I
        NV_RUN_TIMER_I
        NV_DIAL_I
        NV_STACK_I
        NV_STACK_IDX_I
        NV_PAGE_SET_I
        NV_PAGE_MSG_I
        NV_EAR_LVL_I
        NV_SPEAKER_LVL_I
        NV_RINGER_LVL_I
        NV_BEEP_LVL_I
        NV_CALL_BEEP_I
        NV_CONT_KEY_DTMF_I
        NV_CONT_STR_DTMF_I
        NV_SVC_AREA_ALERT_I
        NV_CALL_FADE_ALERT_I
        NV_BANNER_I
        NV_LCD_I
        NV_AUTO_POWER_I
        NV_AUTO_ANSWER_I
        NV_AUTO_REDIAL_I
        NV_AUTO_HYPHEN_I
        NV_BACK_LIGHT_I
        NV_AUTO_MUTE_I
        NV_MAINTRSN_I
        NV_LCKRSN_P_I
        NV_LOCK_I
        NV_LOCK_CODE_I
        NV_AUTO_LOCK_I
        NV_CALL_RSTRC_I
        NV_SEC_CODE_I
        NV_HORN_ALERT_I
        NV_ERR_LOG_I
        NV_UNIT_ID_I
        NV_FREQ_ADJ_I
        NV_VBATT_I
        NV_FM_TX_PWR_I
        NV_FR_TEMP_OFFSET_I
        NV_DM_IO_MODE_I
        NV_CDMA_TX_LIMIT_I
        NV_FM_RSSI_I
        NV_CDMA_RIPPLE_I
        NV_CDMA_RX_OFFSET_I
        NV_CDMA_RX_POWER_I
        NV_CDMA_RX_ERROR_I
        NV_CDMA_TX_SLOPE_1_I
        NV_CDMA_TX_SLOPE_2_I
        NV_CDMA_TX_ERROR_I
        NV_PA_CURRENT_CTL_I
        NV_SONY_ATTEN_1_I
        NV_SONY_ATTEN_2_I
        NV_VOC_GAIN_I
        NV_SPARE_1_I
        NV_SPARE_2_I
        NV_DATA_SRVC_STATE_I
        NV_DATA_IO_MODE_I
        NV_IDLE_DATA_TIMEOUT_I
        NV_MAX_TX_ADJ_I
        NV_INI_MUTE_I
        NV_FACTORY_INFO_I
        NV_SONY_ATTEN_3_I
        NV_SONY_ATTEN_4_I
        NV_SONY_ATTEN_5_I
        NV_DM_ADDR_I
        NV_CDMA_PN_MASK_I
        NV_SEND_TIMEOUT_I
        NV_FM_AGC_SET_VS_PWR_I
        NV_FM_AGC_SET_VS_FREQ_I
        NV_FM_AGC_SET_VS_TEMP_I
        NV_FM_EXP_HDET_VS_PWR_I
        NV_FM_ERR_SLP_VS_PWR_I
        NV_FM_FREQ_SENSE_GAIN_I
        NV_CDMA_RX_LIN_OFF_0_I
        NV_CDMA_RX_LIN_SLP_I
        NV_CDMA_RX_COMP_VS_FREQ_I
        NV_CDMA_TX_COMP_VS_FREQ_I
        NV_CDMA_TX_COMP_VS_VOLT_I
        NV_CDMA_TX_LIN_MASTER_OFF_0_I
        NV_CDMA_TX_LIN_MASTER_SLP_I
        NV_CDMA_TX_LIN_VS_TEMP_I
        NV_CDMA_TX_LIN_VS_VOLT_I
        NV_CDMA_TX_LIM_VS_TEMP_I
        NV_CDMA_TX_LIM_VS_VOLT_I
        NV_CDMA_TX_LIM_VS_FREQ_I
        NV_CDMA_EXP_HDET_VS_AGC_I
        NV_CDMA_ERR_SLP_VS_HDET_I
        NV_THERM_I
        NV_VBATT_PA_I
        NV_HDET_OFF_I
        NV_HDET_SPN_I
        NV_ONETOUCH_DIAL_I
        NV_FM_AGC_ADJ_VS_FREQ_I
        NV_FM_AGC_ADJ_VS_TEMP_I
        NV_RF_CONFIG_I
        NV_R1_RISE_I
        NV_R1_FALL_I
        NV_R2_RISE_I
        NV_R2_FALL_I
        NV_R3_RISE_I
        NV_R3_FALL_I
        NV_PA_RANGE_STEP_CAL_I
        NV_LNA_RANGE_POL_I
        NV_LNA_RANGE_RISE_I
        NV_LNA_RANGE_FALL_I
        NV_LNA_RANGE_OFFSET_I
        NV_POWER_CYCLES_I
        NV_ALERTS_LVL_I
        NV_ALERTS_LVL_SHADOW_I
        NV_RINGER_LVL_SHADOW_I
        NV_BEEP_LVL_SHADOW_I
        NV_EAR_LVL_SHADOW_I
        NV_TIME_SHOW_I
        NV_MESSAGE_ALERT_I
        NV_AIR_CNT_I
        NV_ROAM_CNT_I
        NV_LIFE_CNT_I
        NV_SEND_PIN_I
        NV_AUTO_ANSWER_SHADOW_I
        NV_AUTO_REDIAL_SHADOW_I
        NV_SMS_I
        NV_SMS_DM_I
        NV_IMSI_MCC_I
        NV_IMSI_11_12_I
        NV_DIR_NUMBER_I
        NV_VOICE_PRIV_I
        NV_SPARE_B1_I
        NV_SPARE_B2_I
        NV_SPARE_W1_I
        NV_SPARE_W2_I
        NV_FSC_I
        NV_ALARMS_I
        NV_STANDING_ALARM_I
        NV_ISD_STD_PASSWD_I
        NV_ISD_STD_RESTRICT_I
        NV_DIALING_PLAN_I
        NV_FM_LNA_CTL_I
        NV_LIFE_TIMER_G_I
        NV_CALL_TIMER_G_I
        NV_PWR_DWN_CNT_I
        NV_FM_AGC_I
        NV_FSC2_I
        NV_FSC2_CHKSUM_I
        NV_WDC_I
        NV_HW_CONFIG_I
        NV_CDMA_RX_LIN_VS_TEMP_I
        NV_CDMA_ADJ_FACTOR_I
        NV_CDMA_TX_LIM_BOOSTER_OFF_I
        NV_CDMA_RX_SLP_VS_TEMP_I
        NV_CDMA_TX_SLP_VS_TEMP_I
        NV_PA_RANGE_VS_TEMP_I
        NV_LNA_SWITCH_VS_TEMP_I
        NV_FM_EXP_HDET_VS_TEMP_I
        NV_N1M_I
        NV_IMSI_I
        NV_IMSI_ADDR_NUM_I
        NV_ASSIGNING_TMSI_ZONE_LEN_I
        NV_ASSIGNING_TMSI_ZONE_I
        NV_TMSI_CODE_I
        NV_TMSI_EXP_I
        NV_HOME_PCS_FREQ_BLOCK_I
        NV_DIR_NUMBER_PCS_I
        NV_ROAMING_LIST_I
        NV_MRU_TABLE_I
        NV_REDIAL_I
        NV_OTKSL_I
        NV_TIMED_PREF_MODE_I
        NV_RINGER_TYPE_I
        NV_ANY_KEY_ANSWER_I
        NV_BACK_LIGHT_HFK_I
        NV_RESTRICT_GLOBAL_I
        NV_KEY_SOUND_I
        NV_DIALS_SORTING_METHOD_I
        NV_LANGUAGE_SELECTION_I
        NV_MENU_FORMAT_I
        NV_RINGER_SPKR_LVL_I
        NV_BEEP_SPKR_LVL_I
        NV_MRU2_TABLE_I
        NV_VIBRATOR_I
        NV_FLIP_ANSWERS_I
        NV_DIAL_RESTRICT_LVLS_I
        NV_DIAL_STATE_TABLE_LEN_I
        NV_DIAL_STATE_TABLE_I
        NV_VOICE_PRIV_ALERT_I
        NV_IP_ADDRESS_I
        NV_CURR_GATEWAY_I
        NV_DATA_QNC_ENABLED_I
        NV_DATA_SO_SET_I
        NV_UP_LINK_INFO_I
        NV_UP_PARMS_I
        NV_UP_CACHE_I
        NV_ELAPSED_TIME_I
        NV_PDM2_I
        NV_RX_AGC_MINMAX_I
        NV_VBATT_AUX_I
        NV_DTACO_CONTROL_I
        NV_DTACO_INTERDIGIT_TIMEOUT_I
        NV_PDM1_I
        NV_BELL_MODEM_I
        NV_PDM1_VS_TEMP_I
        NV_PDM2_VS_TEMP_I
        NV_SID_NID_LOCK_I
        NV_PRL_ENABLED_I
        NV_ROAMING_LIST_683_I
        NV_SYSTEM_PREF_I
        NV_HOME_SID_NID_I
        NV_OTAPA_ENABLED_I
        NV_NAM_LOCK_I
        NV_IMSI_T_S1_I
        NV_IMSI_T_S2_I
        NV_IMSI_T_MCC_I
        NV_IMSI_T_11_12_I
        NV_IMSI_T_ADDR_NUM_I
        NV_UP_ALERTS_I
        NV_UP_IDLE_TIMER_I
        NV_SMS_UTC_I
        NV_ROAM_RINGER_I
        NV_RENTAL_TIMER_I
        NV_RENTAL_TIMER_INC_I
        NV_RENTAL_CNT_I
        NV_RENTAL_TIMER_ENABLED_I
        NV_FULL_SYSTEM_PREF_I
        NV_BORSCHT_RINGER_FREQ_I
        NV_PAYPHONE_ENABLE_I
        NV_DSP_ANSWER_DET_ENABLE_I
        NV_EVRC_PRI_I
        NV_AFAX_CLASS_20_I
        NV_V52_CONTROL_I
        NV_CARRIER_INFO_I
        NV_AFAX_I
        NV_SIO_PWRDWN_I
        NV_PREF_VOICE_SO_I
        NV_VRHFK_ENABLED_I
        NV_VRHFK_VOICE_ANSWER_I
        NV_PDM1_VS_FREQ_I
        NV_PDM2_VS_FREQ_I
        NV_SMS_AUTO_DELETE_I
        NV_SRDA_ENABLED_I
        NV_OUTPUT_UI_KEYS_I
        NV_POL_REV_TIMEOUT_I
        NV_SI_TEST_DATA_1_I
        NV_SI_TEST_DATA_2_I
        NV_SPC_CHANGE_ENABLED_I
        NV_DATA_MDR_MODE_I
        NV_DATA_PKT_ORIG_STR_I
        NV_UP_KEY_I
        NV_DATA_AUTO_PACKET_DETECTION_I
        NV_AUTO_VOLUME_ENABLED_I
        NV_WILDCARD_SID_I
        NV_ROAM_MSG_I
        NV_OTKSL_FLAG_I
        NV_BROWSER_TYPE_I
        NV_SMS_REMINDER_TONE_I
        NV_UBROWSER_I
        NV_BTF_ADJUST_I
        NV_FULL_PREF_MODE_I
        NV_UP_BROWSER_WARN_I
        NV_FM_HDET_ADC_RANGE_I
        NV_CDMA_HDET_ADC_RANGE_I
        NV_PN_ID_I
        NV_USER_ZONE_ENABLED_I
        NV_USER_ZONE_I
        NV_PAP_DATA_I
        NV_DATA_DEFAULT_PROFILE_I
        NV_PAP_USER_ID_I = 318
        NV_PAP_PASSWORD_I
        NV_STA_TBYE_I
        NV_STA_MIN_THR_I
        NV_STA_MIN_RX_I
        NV_STA_MIN_ECIO_I
        NV_STA_PRI_I
        NV_PCS_RX_LIN_OFF_0_I = 325
        NV_PCS_RX_LIN_SLP_I = 326
        NV_PCS_RX_COMP_VS_FREQ_I = 327
        NV_PCS_TX_COMP_VS_FREQ_I = 328
        NV_PCS_TX_LIN_MASTER_OFF_0_I = 329
        NV_PCS_TX_LIN_MASTER_SLP_I = 330
        NV_PCS_TX_LIN_VS_TEMP_I = 331
        NV_PCS_TX_LIM_VS_TEMP_I = 332
        NV_PCS_TX_LIM_VS_FREQ_I = 333
        NV_PCS_EXP_HDET_VS_AGC_I = 334
        NV_PCS_HDET_OFF_I = 335
        NV_PCS_HDET_SPN_I = 336
        NV_PCS_R1_RISE_I = 337
        NV_PCS_R1_FALL_I = 338
        NV_PCS_R2_RISE_I = 339
        NV_PCS_R2_FALL_I = 340
        NV_PCS_R3_RISE_I = 341
        NV_PCS_R3_FALL_I = 342
        NV_PCS_PA_RANGE_STEP_CAL_I = 343
        NV_PCS_PDM1_VS_FREQ_I = 344
        NV_PCS_PDM2_VS_FREQ_I = 345
        NV_PCS_LNA_RANGE_POL_I = 346
        NV_PCS_LNA_RANGE_RISE_I = 347
        NV_PCS_LNA_RANGE_FALL_I = 348
        NV_PCS_LNA_RANGE_OFFSET_I = 349
        NV_PCS_RX_LIN_VS_TEMP_I = 350
        NV_PCS_ADJ_FACTOR_I = 351
        NV_PCS_PA_RANGE_VS_TEMP_I = 352
        NV_PCS_PDM1_VS_TEMP_I = 353
        NV_PCS_PDM2_VS_TEMP_I = 354
        NV_PCS_RX_SLP_VS_TEMP_I = 355
        NV_PCS_TX_SLP_VS_TEMP_I = 356
        NV_PCS_RX_AGC_MINMAX_I = 357
        NV_PA_OFFSETS_I = 358
        NV_CDMA_TX_LIN_MASTER_I = 359
        NV_VEXT_I = 360
        NV_VLCD_ADC_CNT_I = 361
        NV_VLCD_DRVR_CNT_I = 362
        NV_VREF_ADJ_PDM_CNT_I = 363
        NV_IBAT_PER_LSB_I = 364
        NV_IEXT_I = 365
        NV_IEXT_THR_I = 366
        NV_CDMA_TX_LIN_MASTER0_I = 367
        NV_CDMA_TX_LIN_MASTER1_I = 368
        NV_CDMA_TX_LIN_MASTER2_I = 369
        NV_CDMA_TX_LIN_MASTER3_I = 370
        NV_TIME_FMT_SELECTION_I = 371
        NV_SMS_BC_SERVICE_TABLE_SIZE_I = 372
        NV_SMS_BC_SERVICE_TABLE_I = 373
        NV_SMS_BC_CONFIG_I = 374
        NV_SMS_BC_USER_PREF_I = 375
        NV_LNA_RANGE_2_RISE_I = 376
        NV_LNA_RANGE_2_FALL_I = 377
        NV_LNA_RANGE_12_OFFSET_I = 378
        NV_NONBYPASS_TIMER_I = 379
        NV_BYPASS_TIMER_I = 380
        NV_IM_LEVEL1_I = 381
        NV_IM_LEVEL2_I = 382
        NV_CDMA_LNA_OFFSET_VS_FREQ_I = 383
        NV_CDMA_LNA_12_OFFSET_VS_FREQ_I = 384
        NV_AGC_PHASE_OFFSET_I = 385
        NV_RX_AGC_MIN_11_I = 386
        NV_PCS_LNA_RANGE_2_RISE_I = 387
        NV_PCS_LNA_RANGE_2_FALL_I = 388
        NV_PCS_LNA_RANGE_12_OFFSET_I = 389
        NV_PCS_NONBYPASS_TIMER_I = 390
        NV_PCS_BYPASS_TIMER_I = 391
        NV_PCS_IM_LEVEL1_I = 392
        NV_PCS_IM_LEVEL2_I = 393
        NV_PCS_CDMA_LNA_OFFSET_VS_FREQ_I = 394
        NV_PCS_CDMA_LNA_12_OFFSET_VS_FREQ_I = 395
        NV_PCS_AGC_PHASE_OFFSET_I = 396
        NV_PCS_RX_AGC_MIN_11_I = 397
        NV_RUIM_CHV_1_I = 398
        NV_RUIM_CHV_2_I = 399
        NV_GPS1_CAPABILITIES_I = 400
        NV_GPS1_PDE_ADDRESS_I = 401
        NV_GPS1_ALLOWED_I = 402
        NV_GPS1_PDE_TRANSPORT_I = 403
        NV_GPS1_MOBILE_CALC_I = 404
        NV_PREF_FOR_RC_I = 405
        NV_SIO_DEV_MAP_MENU_ITEM_I = 408
        NV_TTY_I = 409
        NV_PA_RANGE_OFFSETS_I = 410
        NV_TX_COMP0_I = 411
        NV_MM_SDAC_LVL_I = 412
        NV_BEEP_SDAC_LVL_I = 413
        NV_SDAC_LVL_I = 414
        NV_MM_LVL_I = 415
        NV_MM_LVL_SHADOW_I = 416
        NV_MM_SPEAKER_LVL_I = 417
        NV_MM_PLAY_MODE_I = 418
        NV_MM_REPEAT_MODE_I = 419
        NV_TX_COMP1_I = 420
        NV_TX_COMP2_I = 421
        NV_TX_COMP3_I = 422
        NV_PRIMARY_DNS_I = 423
        NV_SECONDARY_DNS_I = 424
        NV_DIAG_PORT_SELECT_I = 425
        NV_GPS1_PDE_PORT_I = 426
        NV_MM_RINGER_FILE_I = 427
        NV_MM_STORAGE_DEVICE_I = 428
        NV_DATA_SCRM_ENABLED_I = 429
        NV_RUIM_SMS_STATUS_I = 430
        NV_PCS_TX_LIN_MASTER0_I = 431
        NV_PCS_TX_LIN_MASTER1_I = 432
        NV_PCS_TX_LIN_MASTER2_I = 433
        NV_PCS_TX_LIN_MASTER3_I = 434
        NV_PCS_PA_RANGE_OFFSETS_I = 435
        NV_PCS_TX_COMP0_I = 436
        NV_PCS_TX_COMP1_I = 437
        NV_PCS_TX_COMP2_I = 438
        NV_PCS_TX_COMP3_I = 439
        NV_DIAG_RESTART_CONFIG_I = 440
        NV_BAND_PREF_I = 441
        NV_ROAM_PREF_I = 442
        NV_GPS1_GPS_RF_DELAY_I = 443
        NV_GPS1_CDMA_RF_DELAY_I = 444
        NV_PCS_ENC_BTF_I = 445
        NV_CDMA_ENC_BTF_I = 446
        NV_BD_ADDR_I = 447
        NV_SUBPCG_PA_WARMUP_DELAY_I = 448
        NV_GPS1_GPS_RF_LOSS_I = 449
        NV_DATA_TRTL_ENABLED_I = 450
        NV_AMPS_BACKSTOP_ENABLED_I = 451

        NV_RSVD_ITEM_452_I = 452
        NV_RSVD_ITEM_453_I = 453
        NV_DS_DEFAULT_BAUD_I = 454
        NV_DIAG_DEFAULT_BAUD_I = 455
        NV_RSVD_ITEM_456_I = 456
        NV_RSVD_ITEM_457_I = 457
        NV_RSVD_ITEM_458_I = 458
        NV_DS_QCMIP_I = 459
        NV_DS_MIP_RETRIES_I = 460
        NV_DS_MIP_RETRY_INT_I = 461
        NV_DS_MIP_PRE_RE_RRQ_TIME_I = 462
        NV_DS_MIP_NUM_PROF_I = 463
        NV_DS_MIP_ACTIVE_PROF_I = 464
        NV_DS_MIP_GEN_USER_PROF_I = 465
        NV_RSVD_ITEM_466_I = 466
        NV_RSVD_ITEM_467_I = 467
        NV_RSVD_ITEM_468_I = 468
        NV_RSVD_ITEM_469_I = 469
        NV_RSVD_ITEM_470_I = 470
        NV_RSVD_ITEM_471_I = 471
        NV_RSVD_ITEM_472_I = 472
        NV_RSVD_ITEM_473_I = 473
        NV_RSVD_ITEM_474_I = 474
        NV_RSVD_ITEM_475_I = 475
        NV_RSVD_ITEM_476_I = 476
        NV_RSVD_ITEM_477_I = 477
        NV_RSVD_ITEM_478_I = 478
        NV_RSVD_ITEM_479_I = 479
        NV_RSVD_ITEM_480_I = 480
        NV_RSVD_ITEM_481_I = 481
        NV_RSVD_ITEM_482_I = 482
        NV_RSVD_ITEM_483_I = 483
        NV_RSVD_ITEM_484_I = 484
        NV_RSVD_ITEM_485_I = 485
        NV_RSVD_ITEM_486_I = 486
        NV_RSVD_ITEM_487_I = 487
        NV_RSVD_ITEM_488_I = 488
        NV_RSVD_ITEM_489_I = 489
        NV_RSVD_ITEM_490_I = 490
        NV_RSVD_ITEM_491_I = 491
        NV_RSVD_ITEM_492_I = 492
        NV_RSVD_ITEM_493_I = 493
        NV_RSVD_ITEM_494_I = 494
        NV_DS_MIP_QC_DRS_OPT_I = 495
        NV_RSVD_ITEM_496_I = 496
        NV_RSVD_ITEM_497_I = 497
        NV_RSVD_ITEM_498_I = 498
        NV_RSVD_ITEM_499_I = 499
        NV_RSVD_ITEM_500_I = 500
        NV_RING_SOUND_I = 501
        NV_VIB_LVL_I = 502
        NV_MULTILANG_I = 503
        NV_CALL_CONNECT_ALERT_I = 504
        NV_THEME_I = 505
        NV_CARRIER_LOGO_I = 506
        NV_RSVD_ITEM_507_I = 507
        NV_RSVD_ITEM_508_I = 508
        NV_RSVD_ITEM_509_I = 509
        NV_RSVD_ITEM_510_I = 510
        NV_RSVD_ITEM_511_I = 511
        NV_RSVD_ITEM_512_I = 512
        NV_RSVD_ITEM_513_I = 513
        NV_RSVD_ITEM_514_I = 514
        NV_RSVD_ITEM_515_I = 515
        NV_RSVD_ITEM_516_I = 516
        NV_RSVD_ITEM_517_I = 517
        NV_CDMA_TX_LIM_VS_TEMP_FREQ_I = 518
        NV_RSVD_ITEM_519_I = 519
        NV_RSVD_ITEM_520_I = 520
        NV_VOCODER_I = 521
        NV_ENHANCED_RC_I = 522
        NV_PREF_REV_RC_I = 523
        NV_RSVD_ITEM_524_I = 524
        NV_RSVD_ITEM_525_I = 525
        NV_RSVD_ITEM_526_I = 526
        NV_RSVD_ITEM_527_I = 527
        NV_RSVD_ITEM_528_I = 528
        NV_RSVD_ITEM_529_I = 529
        NV_RSVD_ITEM_530_I = 530
        NV_SMS_ALERT_SEL_I = 531
        NV_SMS_2MIN_ALERT_I = 532
        NV_SMS_DEFERRED_SEL_I = 533
        NV_SMS_VALIDITY_SEL_I = 534
        NV_SMS_PRIORITY_SEL_I = 535
        NV_SMS_REPLY_SEL_I = 536
        NV_SMS_DEST_ALERT_SEL_I = 537
        NV_SMS_ORIG_MSG_ID_I = 538
        NV_RSVD_ITEM_539_I = 539
        NV_RSVD_ITEM_540_I = 540
        NV_RSVD_ITEM_541_I = 541
        NV_RSVD_ITEM_542_I = 542
        NV_RSVD_ITEM_543_I = 543
        NV_RSVD_ITEM_544_I = 544
        NV_RSVD_ITEM_545_I = 545
        NV_DS_MIP_2002BIS_MN_HA_AUTH_I = 546
        NV_RSVD_ITEM_547_I = 547
        NV_RSVD_ITEM_548_I = 548
        NV_RSVD_ITEM_549_I = 549
        NV_RSVD_ITEM_550_I = 550
        NV_LNA_GAIN_POL_I = 551
        NV_LNA_GAIN_PWR_MIN_I = 552
        NV_LNA_GAIN_PWR_MAX_I = 553
        NV_CDMA_LNA_LIN_OFF_0_I = 554
        NV_GPS1_LO_CAL_I = 555
        NV_GPS1_ANT_OFF_DB_I = 556
        NV_GPS1_PCS_RF_DELAY_I = 557
        NV_FM_RVC_COMP_VS_FREQ_I = 558
        NV_FM_FSG_VS_TEMP_I = 559
        NV_QDSP_SND_CTRL_I = 560
        NV_AUDIO_ADJ_PHONE_I = 561
        NV_AUDIO_ADJ_EARJACK_I = 562
        NV_AUDIO_ADJ_HFK_I = 563
        NV_RSVD_ITEM_564_I = 564
        NV_RSVD_ITEM_565_I = 565
        NV_RSVD_ITEM_566_I = 566
        NV_RSVD_ITEM_567_I = 567
        NV_RSVD_ITEM_568_I = 568
        NV_RSVD_ITEM_569_I = 569
        NV_RSVD_ITEM_570_I = 570
        NV_RSVD_ITEM_571_I = 571
        NV_RSVD_ITEM_572_I = 572
        NV_RSVD_ITEM_573_I = 573
        NV_RSVD_ITEM_574_I = 574
        NV_RSVD_ITEM_575_I = 575
        NV_RSVD_ITEM_576_I = 576
        NV_RSVD_ITEM_577_I = 577
        NV_RSVD_ITEM_578_I = 578
        NV_HDR_AN_AUTH_NAI_I = 579
        NV_HDR_AN_AUTH_PASSWORD_I = 580
        NV_DS_MIP_ENABLE_PROF_I = 714
        NV_GPS_DOPP_SDEV_I = 736
        NV_PPP_PASSWORD_I = 906
        NV_PPP_USER_ID_I = 910
        NV_HDR_AN_AUTH_PASSWORD_LONG_I = 1192
        NV_HDR_AN_AUTH_USER_ID_LONG_I = 1194
        NV_MEID_I = 1943
        NV_DS_MIP_RM_NAI_I = 2825
        NV_DS_SIP_RM_NAI_I = 2953
        NV_CDMA_SO68_I = &H1006
        NV_WAP_PORT_1_I = &H1429
        NV_WAP_PORT_2_I = &H142A
        NV_WAP_LVL_PORT_1_I = &H142B
        NV_WAP_LVL_PORT_2_I = &H142C
        NV_WAP_DOMAIN_NAME_1_I = &H142D
        NV_WAP_DOMAIN_NAME_2_I = &H142E
        NV_WAP_USER_NAME_I = &H142F
        NV_WAP_PASSWORD_I = &H1430
        NV_WAP_HOMEPAGE_I = &H1431
        NV_WAP_UA_PROF_I = &H1432
        NV_SCR_VERSION_I = &H143F
        NV_MMS_SET_DELIVERY_ACK_I = &H14B7
        'nathan 4.22
        NV_MMS_MMSC_URL_I = &H14BB
        NV_MMS_MMSC_UPLOAD_URL_I = &H14BC
        NV_MMS_MMSC_UAPROF_URL_I = &H14BD
        NV_LG2_MMS_SEND_FROM_ADD_I = &H14BF
        'nathan 4.22
        NV_MMS_MMSC_USER_AGENT_I = &H14C0
        NV_MMS_MMSC_SERVER_PORT_URL_I = &H14C1
        NV_MMS_GATEWAY_SERVER_NAME_IP_I = &H14C6
        NV_MMS_HTTP_METHOD_URL_I = &H14C7
        NV_MMS_MMSC_SECONDARY_NAME_IP_I = &H14C8
        NV_MMS_GATEWAY_PORT_I = &H14CB
        NV_MMS_HTTP_HDR_CONNECTION_I = &H14CC
        NV_MMS_HTTP_HDR_CONTENT_TYPE_I = &H14CD
        NV_MMS_HTTP_HDR_ACCEPT_I = &H14CF
        NV_MMS_HTTP_HDR_ACCEPT_LANGUAGE_I = &H14D0
        NV_MMS_HTTP_HDR_ACCEPT_CHARSET_I = &H14D1
        NV_MMS_HTTP_HDR_NAME_CONTENT_I = &H14D2
        NV_MMS_HTTP_HDR_NAME_USER_AGENT_I = &H14D3
        NV_MMS_HTTP_HDR_NAME_UAPROFILE_I = &H14D4
        NV_MMS_HTTP_HDR_NAME_ACCEPT_I = &H14D5
        NV_MMS_HTTP_HDR_NAME_ACCEPT_LANGUAGE_I = &H14D6
        NV_MMS_HTTP_HDR_NAME_ACCEPT_CHARSET_I = &H14D7
        NV_MMS_HTTP_HDR_NAME_ACCEPT_ENCODING_I = &H14D8
        NV_MMS_HTTP_HDR_NAME_AUTHENTICATION_I = &H14D9
        NV_MMS_HTTP_HDR_NAME_CONNECTION_I = &H14DA
        NV_MMS_HTTP_HDR_NAME_PROXY_AUTHORIZATION_I = &H14DB
        NV_BREW_SERVER_I = &H1519
        NV_BREW_PRIMARY_IP_I = &H151B
        NV_BREW_SECONDARY_IP_I = &H151C
        NV_BREW_CARRIER_ID_I = &H151D
        NV_BREW_BKEY_I = &H151E
        NV_BREW_DOWNLOAD_FLAG_I = &H151F
        NV_BREW_AUTH_POLICY = &H1520
        NV_BREW_PRIVACY_POLICY_I = &H1521
        NV_BREW_PLATFORM_I = &H1523
        NV_BREW_AIRTIME_CHARGE_I = &H1524
        NV_BREW_DISALLOW_DORMANCY = &H1527
        NV_BREW_SUBSCRIBER_ID_LEN_I = &H1528
        NV_FALLBACK_FLAG_I = &H153B
        NV_SIP_DUN_USER_ID_I = &H1F55
        NV_MOTODROID_1F68_I = &H1F68
        NV_MOTODROID_1F69_I = &H1F69
        NV_MOTODROID_1F6A_I = &H1F6A
        NV_MOTODROID_1F6B_I = &H1F6B
        NV_MOTODROID_1F9B_I = &H1F9B

        NV_LG_MMS_ACK_DELIVERY_I = 23003
        NV_LG_MMS_MMSC_URL_I = 23007
        'New NvItems for LG
        NV_LG_MMS_MMSC_UPLOAD_URL_I = 23008
        NV_LG_MMS_MMSC_UAPROF_URL_I = 23009
        NV_LG_MMS_MMSC_USER_AGENT_I = 23010

        NV_LG_MMS_MMSC_SERVER_POST_URL_I = 23011
        NV_LG_MMS_SEND_FROM_ADD_I = 23014
        NV_LG_BROWSER_PROXYSET_PRIMARYPORT_I = 26501
        NV_LG_BROWSER_PROXYSET_SECONDARYPORT_I = 26502
        NV_LG_BROWSER_PROXYSET_PRIMARYDOMAINNAME_I = 26505
        NV_LG_BROWSER_PROXYSET_SECONDARYDOMAINNAME_I = 26506
        NV_LG_BROWSER_PROXYSET_USERNAME_I = 26507
        NV_LG_BROWSER_PROXYSET_PASSWORD_I = 26508
        NV_LG_BROWSER_PROXYSET_HOMEPAGE_I = 26509

        NV_WAP_PORT1_I = 50161
        NV_WAP_PORT2_I = 50162
        NV_WAP_DOMAIN_NAME1_I = 50163
        NV_WAP_DOMAIN_NAME2_I = 50164
        NV_WAP_USERNAME_I = 50165
        NV_WAP_PASSWORD2_I = 50166
        NV_WAP_HOMEPAGE2_I = 50167
        NV_MMS_MMSC_URL2_I = 50307
        NV_MMS_MMSC_UPLOAD_URL2_I = 50308
        NV_MMS_MMSC_UAPROF_URL2_I = 50309
        NV_MMS_SEND_FROM_ADD_I = 50311
        NV_MMS_MMSC_SERVER_POST_URL_I = 50313

        NV_MAX_I
        NV_ITEMS_ENUM_PAD = &H7FFF
    End Enum

    '#Region "NVEnums"

    '    Public Enum NVItems As Integer
    '        ''Random sht for make fix decoder? wat?
    '        NOT_AN_NV_ITEM = -1222

    '        NV_ESN_I = 0
    '        NV_ESN_CHKSUM_I
    '        NV_VERNO_MAJ_I
    '        NV_VERNO_MIN_I
    '        NV_SCM_I
    '        NV_SLOT_CYCLE_INDEX_I
    '        NV_MOB_CAI_REV_I
    '        NV_MOB_FIRM_REV_I
    '        NV_MOB_MODEL_I
    '        NV_CONFIG_CHKSUM_I
    '        NV_PREF_MODE_I
    '        NV_CDMA_PREF_SERV_I
    '        NV_ANALOG_PREF_SERV_I
    '        NV_CDMA_SID_LOCK_I
    '        NV_CDMA_SID_ACQ_I
    '        NV_ANALOG_SID_LOCK_I
    '        NV_ANALOG_SID_ACQ_I
    '        NV_ANALOG_FIRSTCHP_I
    '        NV_ANALOG_HOME_SID_I
    '        NV_ANALOG_REG_I
    '        NV_PCDMACH_I
    '        NV_SCDMACH_I
    '        NV_PPCNCH_I
    '        NV_SPCNCH_I
    '        NV_NAM_CHKSUM_I
    '        NV_A_KEY_I
    '        NV_A_KEY_CHKSUM_I
    '        NV_SSD_A_I
    '        NV_SSD_A_CHKSUM_I
    '        NV_SSD_B_I
    '        NV_SSD_B_CHKSUM_I
    '        NV_COUNT_I
    '        NV_MIN1_I
    '        NV_MIN2_I
    '        NV_MOB_TERM_HOME_I
    '        NV_MOB_TERM_FOR_SID_I
    '        NV_MOB_TERM_FOR_NID_I
    '        NV_ACCOLC_I
    '        NV_SID_NID_I
    '        NV_MIN_CHKSUM_I
    '        NV_CURR_NAM_I
    '        NV_ORIG_MIN_I
    '        NV_AUTO_NAM_I
    '        NV_NAME_NAM_I
    '        NV_NXTREG_I
    '        NV_LSTSID_I
    '        NV_LOCAID_I
    '        NV_PUREG_I
    '        NV_ZONE_LIST_I
    '        NV_SID_NID_LIST_I
    '        NV_DIST_REG_I
    '        NV_LAST_CDMACH_I
    '        NV_CALL_TIMER_I
    '        NV_AIR_TIMER_I
    '        NV_ROAM_TIMER_I
    '        NV_LIFE_TIMER_I
    '        NV_RUN_TIMER_I
    '        NV_DIAL_I
    '        NV_STACK_I
    '        NV_STACK_IDX_I
    '        NV_PAGE_SET_I
    '        NV_PAGE_MSG_I
    '        NV_EAR_LVL_I
    '        NV_SPEAKER_LVL_I
    '        NV_RINGER_LVL_I
    '        NV_BEEP_LVL_I
    '        NV_CALL_BEEP_I
    '        NV_CONT_KEY_DTMF_I
    '        NV_CONT_STR_DTMF_I
    '        NV_SVC_AREA_ALERT_I
    '        NV_CALL_FADE_ALERT_I
    '        NV_BANNER_I
    '        NV_LCD_I
    '        NV_AUTO_POWER_I
    '        NV_AUTO_ANSWER_I
    '        NV_AUTO_REDIAL_I
    '        NV_AUTO_HYPHEN_I
    '        NV_BACK_LIGHT_I
    '        NV_AUTO_MUTE_I
    '        NV_MAINTRSN_I
    '        NV_LCKRSN_P_I
    '        NV_LOCK_I
    '        NV_LOCK_CODE_I
    '        NV_AUTO_LOCK_I
    '        NV_CALL_RSTRC_I
    '        NV_SEC_CODE_I
    '        NV_HORN_ALERT_I
    '        NV_ERR_LOG_I
    '        NV_UNIT_ID_I
    '        NV_FREQ_ADJ_I
    '        NV_VBATT_I
    '        NV_FM_TX_PWR_I
    '        NV_FR_TEMP_OFFSET_I
    '        NV_DM_IO_MODE_I
    '        NV_CDMA_TX_LIMIT_I
    '        NV_FM_RSSI_I
    '        NV_CDMA_RIPPLE_I
    '        NV_CDMA_RX_OFFSET_I
    '        NV_CDMA_RX_POWER_I
    '        NV_CDMA_RX_ERROR_I
    '        NV_CDMA_TX_SLOPE_1_I
    '        NV_CDMA_TX_SLOPE_2_I
    '        NV_CDMA_TX_ERROR_I
    '        NV_PA_CURRENT_CTL_I
    '        NV_SONY_ATTEN_1_I
    '        NV_SONY_ATTEN_2_I
    '        NV_VOC_GAIN_I
    '        NV_SPARE_1_I
    '        NV_SPARE_2_I
    '        NV_DATA_SRVC_STATE_I
    '        NV_DATA_IO_MODE_I
    '        NV_IDLE_DATA_TIMEOUT_I
    '        NV_MAX_TX_ADJ_I
    '        NV_INI_MUTE_I
    '        NV_FACTORY_INFO_I
    '        NV_SONY_ATTEN_3_I
    '        NV_SONY_ATTEN_4_I
    '        NV_SONY_ATTEN_5_I
    '        NV_DM_ADDR_I
    '        NV_CDMA_PN_MASK_I
    '        NV_SEND_TIMEOUT_I
    '        NV_FM_AGC_SET_VS_PWR_I
    '        NV_FM_AGC_SET_VS_FREQ_I
    '        NV_FM_AGC_SET_VS_TEMP_I
    '        NV_FM_EXP_HDET_VS_PWR_I
    '        NV_FM_ERR_SLP_VS_PWR_I
    '        NV_FM_FREQ_SENSE_GAIN_I
    '        NV_CDMA_RX_LIN_OFF_0_I
    '        NV_CDMA_RX_LIN_SLP_I
    '        NV_CDMA_RX_COMP_VS_FREQ_I
    '        NV_CDMA_TX_COMP_VS_FREQ_I
    '        NV_CDMA_TX_COMP_VS_VOLT_I
    '        NV_CDMA_TX_LIN_MASTER_OFF_0_I
    '        NV_CDMA_TX_LIN_MASTER_SLP_I
    '        NV_CDMA_TX_LIN_VS_TEMP_I
    '        NV_CDMA_TX_LIN_VS_VOLT_I
    '        NV_CDMA_TX_LIM_VS_TEMP_I
    '        NV_CDMA_TX_LIM_VS_VOLT_I
    '        NV_CDMA_TX_LIM_VS_FREQ_I
    '        NV_CDMA_EXP_HDET_VS_AGC_I
    '        NV_CDMA_ERR_SLP_VS_HDET_I
    '        NV_THERM_I
    '        NV_VBATT_PA_I
    '        NV_HDET_OFF_I
    '        NV_HDET_SPN_I
    '        NV_ONETOUCH_DIAL_I
    '        NV_FM_AGC_ADJ_VS_FREQ_I
    '        NV_FM_AGC_ADJ_VS_TEMP_I
    '        NV_RF_CONFIG_I
    '        NV_R1_RISE_I
    '        NV_R1_FALL_I
    '        NV_R2_RISE_I
    '        NV_R2_FALL_I
    '        NV_R3_RISE_I
    '        NV_R3_FALL_I
    '        NV_PA_RANGE_STEP_CAL_I
    '        NV_LNA_RANGE_POL_I
    '        NV_LNA_RANGE_RISE_I
    '        NV_LNA_RANGE_FALL_I
    '        NV_LNA_RANGE_OFFSET_I
    '        NV_POWER_CYCLES_I
    '        NV_ALERTS_LVL_I
    '        NV_ALERTS_LVL_SHADOW_I
    '        NV_RINGER_LVL_SHADOW_I
    '        NV_BEEP_LVL_SHADOW_I
    '        NV_EAR_LVL_SHADOW_I
    '        NV_TIME_SHOW_I
    '        NV_MESSAGE_ALERT_I
    '        NV_AIR_CNT_I
    '        NV_ROAM_CNT_I
    '        NV_LIFE_CNT_I
    '        NV_SEND_PIN_I
    '        NV_AUTO_ANSWER_SHADOW_I
    '        NV_AUTO_REDIAL_SHADOW_I
    '        NV_SMS_I
    '        NV_SMS_DM_I
    '        NV_IMSI_MCC_I
    '        NV_IMSI_11_12_I
    '        NV_DIR_NUMBER_I
    '        NV_VOICE_PRIV_I
    '        NV_SPARE_B1_I
    '        NV_SPARE_B2_I
    '        NV_SPARE_W1_I
    '        NV_SPARE_W2_I
    '        NV_FSC_I
    '        NV_ALARMS_I
    '        NV_STANDING_ALARM_I
    '        NV_ISD_STD_PASSWD_I
    '        NV_ISD_STD_RESTRICT_I
    '        NV_DIALING_PLAN_I
    '        NV_FM_LNA_CTL_I
    '        NV_LIFE_TIMER_G_I
    '        NV_CALL_TIMER_G_I
    '        NV_PWR_DWN_CNT_I
    '        NV_FM_AGC_I
    '        NV_FSC2_I
    '        NV_FSC2_CHKSUM_I
    '        NV_WDC_I
    '        NV_HW_CONFIG_I
    '        NV_CDMA_RX_LIN_VS_TEMP_I
    '        NV_CDMA_ADJ_FACTOR_I
    '        NV_CDMA_TX_LIM_BOOSTER_OFF_I
    '        NV_CDMA_RX_SLP_VS_TEMP_I
    '        NV_CDMA_TX_SLP_VS_TEMP_I
    '        NV_PA_RANGE_VS_TEMP_I
    '        NV_LNA_SWITCH_VS_TEMP_I
    '        NV_FM_EXP_HDET_VS_TEMP_I
    '        NV_N1M_I
    '        NV_IMSI_I
    '        NV_IMSI_ADDR_NUM_I
    '        NV_ASSIGNING_TMSI_ZONE_LEN_I
    '        NV_ASSIGNING_TMSI_ZONE_I
    '        NV_TMSI_CODE_I
    '        NV_TMSI_EXP_I
    '        NV_HOME_PCS_FREQ_BLOCK_I
    '        NV_DIR_NUMBER_PCS_I
    '        NV_ROAMING_LIST_I
    '        NV_MRU_TABLE_I
    '        NV_REDIAL_I
    '        NV_OTKSL_I
    '        NV_TIMED_PREF_MODE_I
    '        NV_RINGER_TYPE_I
    '        NV_ANY_KEY_ANSWER_I
    '        NV_BACK_LIGHT_HFK_I
    '        NV_RESTRICT_GLOBAL_I
    '        NV_KEY_SOUND_I
    '        NV_DIALS_SORTING_METHOD_I
    '        NV_LANGUAGE_SELECTION_I
    '        NV_MENU_FORMAT_I
    '        NV_RINGER_SPKR_LVL_I
    '        NV_BEEP_SPKR_LVL_I
    '        NV_MRU2_TABLE_I
    '        NV_VIBRATOR_I
    '        NV_FLIP_ANSWERS_I
    '        NV_DIAL_RESTRICT_LVLS_I
    '        NV_DIAL_STATE_TABLE_LEN_I
    '        NV_DIAL_STATE_TABLE_I
    '        NV_VOICE_PRIV_ALERT_I
    '        NV_IP_ADDRESS_I
    '        NV_CURR_GATEWAY_I
    '        NV_DATA_QNC_ENABLED_I
    '        NV_DATA_SO_SET_I
    '        NV_UP_LINK_INFO_I
    '        NV_UP_PARMS_I
    '        NV_UP_CACHE_I
    '        NV_ELAPSED_TIME_I
    '        NV_PDM2_I
    '        NV_RX_AGC_MINMAX_I
    '        NV_VBATT_AUX_I
    '        NV_DTACO_CONTROL_I
    '        NV_DTACO_INTERDIGIT_TIMEOUT_I
    '        NV_PDM1_I
    '        NV_BELL_MODEM_I
    '        NV_PDM1_VS_TEMP_I
    '        NV_PDM2_VS_TEMP_I
    '        NV_SID_NID_LOCK_I
    '        NV_PRL_ENABLED_I
    '        NV_ROAMING_LIST_683_I
    '        NV_SYSTEM_PREF_I
    '        NV_HOME_SID_NID_I
    '        NV_OTAPA_ENABLED_I
    '        NV_NAM_LOCK_I
    '        NV_IMSI_T_S1_I
    '        NV_IMSI_T_S2_I
    '        NV_IMSI_T_MCC_I
    '        NV_IMSI_T_11_12_I
    '        NV_IMSI_T_ADDR_NUM_I
    '        NV_UP_ALERTS_I
    '        NV_UP_IDLE_TIMER_I
    '        NV_SMS_UTC_I
    '        NV_ROAM_RINGER_I
    '        NV_RENTAL_TIMER_I
    '        NV_RENTAL_TIMER_INC_I
    '        NV_RENTAL_CNT_I
    '        NV_RENTAL_TIMER_ENABLED_I
    '        NV_FULL_SYSTEM_PREF_I
    '        NV_BORSCHT_RINGER_FREQ_I
    '        NV_PAYPHONE_ENABLE_I
    '        NV_DSP_ANSWER_DET_ENABLE_I
    '        NV_EVRC_PRI_I
    '        NV_AFAX_CLASS_20_I
    '        NV_V52_CONTROL_I
    '        NV_CARRIER_INFO_I
    '        NV_AFAX_I
    '        NV_SIO_PWRDWN_I
    '        NV_PREF_VOICE_SO_I
    '        NV_VRHFK_ENABLED_I
    '        NV_VRHFK_VOICE_ANSWER_I
    '        NV_PDM1_VS_FREQ_I
    '        NV_PDM2_VS_FREQ_I
    '        NV_SMS_AUTO_DELETE_I
    '        NV_SRDA_ENABLED_I
    '        NV_OUTPUT_UI_KEYS_I
    '        NV_POL_REV_TIMEOUT_I
    '        NV_SI_TEST_DATA_1_I
    '        NV_SI_TEST_DATA_2_I
    '        NV_SPC_CHANGE_ENABLED_I
    '        NV_DATA_MDR_MODE_I
    '        NV_DATA_PKT_ORIG_STR_I
    '        NV_UP_KEY_I
    '        NV_DATA_AUTO_PACKET_DETECTION_I
    '        NV_AUTO_VOLUME_ENABLED_I
    '        NV_WILDCARD_SID_I
    '        NV_ROAM_MSG_I
    '        NV_OTKSL_FLAG_I
    '        NV_BROWSER_TYPE_I
    '        NV_SMS_REMINDER_TONE_I
    '        NV_UBROWSER_I
    '        NV_BTF_ADJUST_I
    '        NV_FULL_PREF_MODE_I
    '        NV_UP_BROWSER_WARN_I
    '        NV_FM_HDET_ADC_RANGE_I
    '        NV_CDMA_HDET_ADC_RANGE_I
    '        NV_PN_ID_I
    '        NV_USER_ZONE_ENABLED_I
    '        NV_USER_ZONE_I
    '        NV_PAP_DATA_I
    '        NV_DATA_DEFAULT_PROFILE_I
    '        NV_PAP_USER_ID_I
    '        NV_PAP_PASSWORD_I
    '        NV_STA_TBYE_I
    '        NV_STA_MIN_THR_I
    '        NV_STA_MIN_RX_I
    '        NV_STA_MIN_ECIO_I
    '        NV_STA_PRI_I
    '        NV_PCS_RX_LIN_OFF_0_I = 325
    '        NV_PCS_RX_LIN_SLP_I = 326
    '        NV_PCS_RX_COMP_VS_FREQ_I = 327
    '        NV_PCS_TX_COMP_VS_FREQ_I = 328
    '        NV_PCS_TX_LIN_MASTER_OFF_0_I = 329
    '        NV_PCS_TX_LIN_MASTER_SLP_I = 330
    '        NV_PCS_TX_LIN_VS_TEMP_I = 331
    '        NV_PCS_TX_LIM_VS_TEMP_I = 332
    '        NV_PCS_TX_LIM_VS_FREQ_I = 333
    '        NV_PCS_EXP_HDET_VS_AGC_I = 334
    '        NV_PCS_HDET_OFF_I = 335
    '        NV_PCS_HDET_SPN_I = 336
    '        NV_PCS_R1_RISE_I = 337
    '        NV_PCS_R1_FALL_I = 338
    '        NV_PCS_R2_RISE_I = 339
    '        NV_PCS_R2_FALL_I = 340
    '        NV_PCS_R3_RISE_I = 341
    '        NV_PCS_R3_FALL_I = 342
    '        NV_PCS_PA_RANGE_STEP_CAL_I = 343
    '        NV_PCS_PDM1_VS_FREQ_I = 344
    '        NV_PCS_PDM2_VS_FREQ_I = 345
    '        NV_PCS_LNA_RANGE_POL_I = 346
    '        NV_PCS_LNA_RANGE_RISE_I = 347
    '        NV_PCS_LNA_RANGE_FALL_I = 348
    '        NV_PCS_LNA_RANGE_OFFSET_I = 349
    '        NV_PCS_RX_LIN_VS_TEMP_I = 350
    '        NV_PCS_ADJ_FACTOR_I = 351
    '        NV_PCS_PA_RANGE_VS_TEMP_I = 352
    '        NV_PCS_PDM1_VS_TEMP_I = 353
    '        NV_PCS_PDM2_VS_TEMP_I = 354
    '        NV_PCS_RX_SLP_VS_TEMP_I = 355
    '        NV_PCS_TX_SLP_VS_TEMP_I = 356
    '        NV_PCS_RX_AGC_MINMAX_I = 357
    '        NV_PA_OFFSETS_I = 358
    '        NV_CDMA_TX_LIN_MASTER_I = 359
    '        NV_VEXT_I = 360
    '        NV_VLCD_ADC_CNT_I = 361
    '        NV_VLCD_DRVR_CNT_I = 362
    '        NV_VREF_ADJ_PDM_CNT_I = 363
    '        NV_IBAT_PER_LSB_I = 364
    '        NV_IEXT_I = 365
    '        NV_IEXT_THR_I = 366
    '        NV_CDMA_TX_LIN_MASTER0_I = 367
    '        NV_CDMA_TX_LIN_MASTER1_I = 368
    '        NV_CDMA_TX_LIN_MASTER2_I = 369
    '        NV_CDMA_TX_LIN_MASTER3_I = 370
    '        NV_TIME_FMT_SELECTION_I = 371
    '        NV_SMS_BC_SERVICE_TABLE_SIZE_I = 372
    '        NV_SMS_BC_SERVICE_TABLE_I = 373
    '        NV_SMS_BC_CONFIG_I = 374
    '        NV_SMS_BC_USER_PREF_I = 375
    '        NV_LNA_RANGE_2_RISE_I = 376
    '        NV_LNA_RANGE_2_FALL_I = 377
    '        NV_LNA_RANGE_12_OFFSET_I = 378
    '        NV_NONBYPASS_TIMER_I = 379
    '        NV_BYPASS_TIMER_I = 380
    '        NV_IM_LEVEL1_I = 381
    '        NV_IM_LEVEL2_I = 382
    '        NV_CDMA_LNA_OFFSET_VS_FREQ_I = 383
    '        NV_CDMA_LNA_12_OFFSET_VS_FREQ_I = 384
    '        NV_AGC_PHASE_OFFSET_I = 385
    '        NV_RX_AGC_MIN_11_I = 386
    '        NV_PCS_LNA_RANGE_2_RISE_I = 387
    '        NV_PCS_LNA_RANGE_2_FALL_I = 388
    '        NV_PCS_LNA_RANGE_12_OFFSET_I = 389
    '        NV_PCS_NONBYPASS_TIMER_I = 390
    '        NV_PCS_BYPASS_TIMER_I = 391
    '        NV_PCS_IM_LEVEL1_I = 392
    '        NV_PCS_IM_LEVEL2_I = 393
    '        NV_PCS_CDMA_LNA_OFFSET_VS_FREQ_I = 394
    '        NV_PCS_CDMA_LNA_12_OFFSET_VS_FREQ_I = 395
    '        NV_PCS_AGC_PHASE_OFFSET_I = 396
    '        NV_PCS_RX_AGC_MIN_11_I = 397
    '        NV_RUIM_CHV_1_I = 398
    '        NV_RUIM_CHV_2_I = 399
    '        NV_GPS1_CAPABILITIES_I = 400
    '        NV_GPS1_PDE_ADDRESS_I = 401
    '        NV_GPS1_ALLOWED_I = 402
    '        NV_GPS1_PDE_TRANSPORT_I = 403
    '        NV_GPS1_MOBILE_CALC_I = 404
    '        NV_PREF_FOR_RC_I = 405
    '        NV_SIO_DEV_MAP_MENU_ITEM_I = 408
    '        NV_TTY_I = 409
    '        NV_PA_RANGE_OFFSETS_I = 410
    '        NV_TX_COMP0_I = 411
    '        NV_MM_SDAC_LVL_I = 412
    '        NV_BEEP_SDAC_LVL_I = 413
    '        NV_SDAC_LVL_I = 414
    '        NV_MM_LVL_I = 415
    '        NV_MM_LVL_SHADOW_I = 416
    '        NV_MM_SPEAKER_LVL_I = 417
    '        NV_MM_PLAY_MODE_I = 418
    '        NV_MM_REPEAT_MODE_I = 419
    '        NV_TX_COMP1_I = 420
    '        NV_TX_COMP2_I = 421
    '        NV_TX_COMP3_I = 422
    '        NV_PRIMARY_DNS_I = 423
    '        NV_SECONDARY_DNS_I = 424
    '        NV_DIAG_PORT_SELECT_I = 425
    '        NV_GPS1_PDE_PORT_I = 426
    '        NV_MM_RINGER_FILE_I = 427
    '        NV_MM_STORAGE_DEVICE_I = 428
    '        NV_DATA_SCRM_ENABLED_I = 429
    '        NV_RUIM_SMS_STATUS_I = 430
    '        NV_PCS_TX_LIN_MASTER0_I = 431
    '        NV_PCS_TX_LIN_MASTER1_I = 432
    '        NV_PCS_TX_LIN_MASTER2_I = 433
    '        NV_PCS_TX_LIN_MASTER3_I = 434
    '        NV_PCS_PA_RANGE_OFFSETS_I = 435
    '        NV_PCS_TX_COMP0_I = 436
    '        NV_PCS_TX_COMP1_I = 437
    '        NV_PCS_TX_COMP2_I = 438
    '        NV_PCS_TX_COMP3_I = 439
    '        NV_DIAG_RESTART_CONFIG_I = 440
    '        NV_BAND_PREF_I = 441
    '        NV_ROAM_PREF_I = 442
    '        NV_GPS1_GPS_RF_DELAY_I = 443
    '        NV_GPS1_CDMA_RF_DELAY_I = 444
    '        NV_PCS_ENC_BTF_I = 445
    '        NV_CDMA_ENC_BTF_I = 446
    '        NV_BD_ADDR_I = 447
    '        NV_SUBPCG_PA_WARMUP_DELAY_I = 448
    '        NV_GPS1_GPS_RF_LOSS_I = 449
    '        NV_DATA_TRTL_ENABLED_I = 450
    '        NV_AMPS_BACKSTOP_ENABLED_I = 451

    '        NV_RSVD_ITEM_452_I = 452
    '        NV_RSVD_ITEM_453_I = 453
    '        NV_DS_DEFAULT_BAUD_I = 454
    '        NV_DIAG_DEFAULT_BAUD_I = 455
    '        NV_RSVD_ITEM_456_I = 456
    '        NV_RSVD_ITEM_457_I = 457
    '        NV_RSVD_ITEM_458_I = 458
    '        NV_DS_QCMIP_I = 459
    '        NV_DS_MIP_RETRIES_I = 460
    '        NV_DS_MIP_RETRY_INT_I = 461
    '        NV_DS_MIP_PRE_RE_RRQ_TIME_I = 462
    '        NV_DS_MIP_NUM_PROF_I = 463
    '        NV_DS_MIP_ACTIVE_PROF_I = 464
    '        NV_DS_MIP_GEN_USER_PROF_I = 465
    '        NV_RSVD_ITEM_466_I = 466
    '        NV_RSVD_ITEM_467_I = 467
    '        NV_RSVD_ITEM_468_I = 468
    '        NV_RSVD_ITEM_469_I = 469
    '        NV_RSVD_ITEM_470_I = 470
    '        NV_RSVD_ITEM_471_I = 471
    '        NV_RSVD_ITEM_472_I = 472
    '        NV_RSVD_ITEM_473_I = 473
    '        NV_RSVD_ITEM_474_I = 474
    '        NV_RSVD_ITEM_475_I = 475
    '        NV_RSVD_ITEM_476_I = 476
    '        NV_RSVD_ITEM_477_I = 477
    '        NV_RSVD_ITEM_478_I = 478
    '        NV_RSVD_ITEM_479_I = 479
    '        NV_RSVD_ITEM_480_I = 480
    '        NV_RSVD_ITEM_481_I = 481
    '        NV_RSVD_ITEM_482_I = 482
    '        NV_RSVD_ITEM_483_I = 483
    '        NV_RSVD_ITEM_484_I = 484
    '        NV_RSVD_ITEM_485_I = 485
    '        NV_RSVD_ITEM_486_I = 486
    '        NV_RSVD_ITEM_487_I = 487
    '        NV_RSVD_ITEM_488_I = 488
    '        NV_RSVD_ITEM_489_I = 489
    '        NV_RSVD_ITEM_490_I = 490
    '        NV_RSVD_ITEM_491_I = 491
    '        NV_RSVD_ITEM_492_I = 492
    '        NV_RSVD_ITEM_493_I = 493
    '        NV_RSVD_ITEM_494_I = 494
    '        NV_DS_MIP_QC_DRS_OPT_I = 495
    '        NV_RSVD_ITEM_496_I = 496
    '        NV_RSVD_ITEM_497_I = 497
    '        NV_RSVD_ITEM_498_I = 498
    '        NV_RSVD_ITEM_499_I = 499
    '        NV_RSVD_ITEM_500_I = 500
    '        NV_RING_SOUND_I = 501
    '        NV_VIB_LVL_I = 502
    '        NV_MULTILANG_I = 503
    '        NV_CALL_CONNECT_ALERT_I = 504
    '        NV_THEME_I = 505
    '        NV_CARRIER_LOGO_I = 506
    '        NV_RSVD_ITEM_507_I = 507
    '        NV_RSVD_ITEM_508_I = 508
    '        NV_RSVD_ITEM_509_I = 509
    '        NV_RSVD_ITEM_510_I = 510
    '        NV_RSVD_ITEM_511_I = 511
    '        NV_RSVD_ITEM_512_I = 512
    '        NV_RSVD_ITEM_513_I = 513
    '        NV_RSVD_ITEM_514_I = 514
    '        NV_RSVD_ITEM_515_I = 515
    '        NV_RSVD_ITEM_516_I = 516
    '        NV_RSVD_ITEM_517_I = 517
    '        NV_CDMA_TX_LIM_VS_TEMP_FREQ_I = 518
    '        NV_RSVD_ITEM_519_I = 519
    '        NV_RSVD_ITEM_520_I = 520
    '        NV_VOCODER_I = 521
    '        NV_ENHANCED_RC_I = 522
    '        NV_PREF_REV_RC_I = 523
    '        NV_RSVD_ITEM_524_I = 524
    '        NV_RSVD_ITEM_525_I = 525
    '        NV_RSVD_ITEM_526_I = 526
    '        NV_RSVD_ITEM_527_I = 527
    '        NV_RSVD_ITEM_528_I = 528
    '        NV_RSVD_ITEM_529_I = 529
    '        NV_RSVD_ITEM_530_I = 530
    '        NV_SMS_ALERT_SEL_I = 531
    '        NV_SMS_2MIN_ALERT_I = 532
    '        NV_SMS_DEFERRED_SEL_I = 533
    '        NV_SMS_VALIDITY_SEL_I = 534
    '        NV_SMS_PRIORITY_SEL_I = 535
    '        NV_SMS_REPLY_SEL_I = 536
    '        NV_SMS_DEST_ALERT_SEL_I = 537
    '        NV_SMS_ORIG_MSG_ID_I = 538
    '        NV_RSVD_ITEM_539_I = 539
    '        NV_RSVD_ITEM_540_I = 540
    '        NV_RSVD_ITEM_541_I = 541
    '        NV_RSVD_ITEM_542_I = 542
    '        NV_RSVD_ITEM_543_I = 543
    '        NV_RSVD_ITEM_544_I = 544
    '        NV_RSVD_ITEM_545_I = 545
    '        NV_DS_MIP_2002BIS_MN_HA_AUTH_I = 546
    '        NV_RSVD_ITEM_547_I = 547
    '        NV_RSVD_ITEM_548_I = 548
    '        NV_RSVD_ITEM_549_I = 549
    '        NV_RSVD_ITEM_550_I = 550
    '        NV_LNA_GAIN_POL_I = 551
    '        NV_LNA_GAIN_PWR_MIN_I = 552
    '        NV_LNA_GAIN_PWR_MAX_I = 553
    '        NV_CDMA_LNA_LIN_OFF_0_I = 554
    '        NV_GPS1_LO_CAL_I = 555
    '        NV_GPS1_ANT_OFF_DB_I = 556
    '        NV_GPS1_PCS_RF_DELAY_I = 557
    '        NV_FM_RVC_COMP_VS_FREQ_I = 558
    '        NV_FM_FSG_VS_TEMP_I = 559
    '        NV_QDSP_SND_CTRL_I = 560
    '        NV_AUDIO_ADJ_PHONE_I = 561
    '        NV_AUDIO_ADJ_EARJACK_I = 562
    '        NV_AUDIO_ADJ_HFK_I = 563
    '        NV_RSVD_ITEM_564_I = 564
    '        NV_RSVD_ITEM_565_I = 565
    '        NV_RSVD_ITEM_566_I = 566
    '        NV_RSVD_ITEM_567_I = 567
    '        NV_RSVD_ITEM_568_I = 568
    '        NV_RSVD_ITEM_569_I = 569
    '        NV_RSVD_ITEM_570_I = 570
    '        NV_RSVD_ITEM_571_I = 571
    '        NV_RSVD_ITEM_572_I = 572
    '        NV_RSVD_ITEM_573_I = 573
    '        NV_RSVD_ITEM_574_I = 574
    '        NV_RSVD_ITEM_575_I = 575
    '        NV_RSVD_ITEM_576_I = 576
    '        NV_RSVD_ITEM_577_I = 577
    '        NV_RSVD_ITEM_578_I = 578

    '        ''http://read.pudn.com/downloads125/sourcecode/windows/comm/529045/CDMA%E6%B5%8B%E8%AF%95%E5%B7%A5%E5%85%B7v2.6.3/Include/NV.h__.htm
    '        ''/* Stores the password for 1xEV(HDR) Access Network CHAP Authentication  */
    '        NV_HDR_AN_AUTH_NAI_I = 579
    '        NV_HDR_AN_AUTH_PASSWORD_I = 580                                       ''  /*580*/ 
    '        ''NV_RSVD_ITEM_579_I = 579
    '        ''NV_RSVD_ITEM_580_I = 580
    '        NV_DS_MIP_ENABLE_PROF_I = 714
    '        NV_GPS_DOPP_SDEV_I = 736
    '        NV_PPP_PASSWORD_I = 906
    '        NV_PPP_USER_ID_I = 910

    '        ''dg TODO test hdr
    '        NV_HDR_AN_AUTH_PASSWD_LONG = 1192
    '        NV_HDR_AN_AUTH_USER_ID_LONG = 1194

    '        NV_MEID_I = 1943
    '        NV_DS_MIP_RM_NAI_I = 2825
    '        NV_DS_SIP_RM_NAI_I = 2953
    '        NV_CDMA_SO68_I = &H1006
    '        NV_WAP_PORT_1_I = &H1429
    '        NV_WAP_PORT_2_I = &H142A
    '        NV_WAP_LVL_PORT_1_I = &H142B
    '        NV_WAP_LVL_PORT_2_I = &H142C
    '        NV_WAP_DOMAIN_NAME_1_I = &H142D
    '        NV_WAP_DOMAIN_NAME_2_I = &H142E
    '        NV_WAP_USER_NAME_I = &H142F
    '        NV_WAP_PASSWORD_I = &H1430
    '        NV_WAP_HOMEPAGE_I = &H1431
    '        NV_WAP_UA_PROF_I = &H1432
    '        NV_SCR_VERSION_I = &H143F
    '        NV_MMS_MMSC_URL_I = &H14BB
    '        NV_MMS_MMSC_UPLOAD_URL_I = &H14BC
    '        NV_MMS_MMSC_UAPROF_URL_I = &H14BD
    '        NV_MMS_MMSC_USER_AGENT_I = &H14C0
    '        NV_MMS_MMSC_SERVER_PORT_URL_I = &H14C1
    '        NV_MMS_GATEWAY_SERVER_NAME_IP_I = &H14C6
    '        NV_MMS_HTTP_METHOD_URL_I = &H14C7
    '        NV_MMS_MMSC_SECONDARY_NAME_IP_I = &H14C8
    '        NV_MMS_GATEWAY_PORT_I = &H14CB
    '        NV_MMS_HTTP_HDR_CONNECTION_I = &H14CC
    '        NV_MMS_HTTP_HDR_CONTENT_TYPE_I = &H14CD
    '        NV_MMS_HTTP_HDR_ACCEPT_I = &H14CF
    '        NV_MMS_HTTP_HDR_ACCEPT_LANGUAGE_I = &H14D0
    '        NV_MMS_HTTP_HDR_ACCEPT_CHARSET_I = &H14D1
    '        NV_MMS_HTTP_HDR_NAME_CONTENT_I = &H14D2
    '        NV_MMS_HTTP_HDR_NAME_USER_AGENT_I = &H14D3
    '        NV_MMS_HTTP_HDR_NAME_UAPROFILE_I = &H14D4
    '        NV_MMS_HTTP_HDR_NAME_ACCEPT_I = &H14D5
    '        NV_MMS_HTTP_HDR_NAME_ACCEPT_LANGUAGE_I = &H14D6
    '        NV_MMS_HTTP_HDR_NAME_ACCEPT_CHARSET_I = &H14D7
    '        NV_MMS_HTTP_HDR_NAME_ACCEPT_ENCODING_I = &H14D8
    '        NV_MMS_HTTP_HDR_NAME_AUTHENTICATION_I = &H14D9
    '        NV_MMS_HTTP_HDR_NAME_CONNECTION_I = &H14DA
    '        NV_MMS_HTTP_HDR_NAME_PROXY_AUTHORIZATION_I = &H14DB
    '        NV_BREW_SERVER_I = &H1519
    '        NV_BREW_PRIMARY_IP_I = &H151B
    '        NV_BREW_SECONDARY_IP_I = &H151C
    '        NV_BREW_CARRIER_ID_I = &H151D
    '        NV_BREW_BKEY_I = &H151E
    '        NV_BREW_DOWNLOAD_FLAG_I = &H151F
    '        NV_BREW_AUTH_POLICY = &H1520
    '        NV_BREW_PRIVACY_POLICY_I = &H1521
    '        NV_BREW_PLATFORM_I = &H1523
    '        NV_BREW_AIRTIME_CHARGE_I = &H1524
    '        NV_BREW_DISALLOW_DORMANCY = &H1527
    '        NV_BREW_SUBSCRIBER_ID_LEN_I = &H1528
    '        NV_FALLBACK_FLAG_I = &H153B
    '        NV_MAX_I
    '        NV_ITEMS_ENUM_PAD = &H7FFF


    '    End Enum

    '    Public Enum FSItems As Short
    '        DIAG_SUBSYS_LG_FS_ITEM = &H81
    '    End Enum

    '#End Region

    Function fixNVItemNumber(ByVal itemNumber As Integer, ByVal length As Integer) As String
        Dim chex As String = Convert.ToString(itemNumber, 16)
        ''make it fatter
        ''mmm... food

        ''hm. lazy. sure theres a betta way but whateva
        If (chex.Length = 1) And (length = 6) Then
            chex = "00000" + chex
        ElseIf (chex.Length = 2) And (length = 6) Then
            chex = "0000" + chex


        ElseIf (chex.Length = 3 Or ((length = 4) And chex.Length = 1)) Then
            chex = "000" + chex
        ElseIf (chex.Length = 4 Or ((length = 4) And chex.Length = 2)) Then
            chex = "00" + chex
        ElseIf (chex.Length = 5 Or ((length = 4) And chex.Length = 3)) Then
            chex = "0" + chex
        End If

        ''debug:looks ok
        '' logger.addToLog("post length fix?: " + chex)

        ''fix the order the hex is displayed in

        ''TODO: fcked off need to check if item length is 4 or 6
        Dim returnHex As String = ""
        If length = 6 Then
            returnHex += chex(4)
            returnHex += chex(5)
        End If

        returnHex += chex(2)
        returnHex += chex(3)
        returnHex += chex(0)
        returnHex += chex(1)

        ''logger.addToLog("return hex revd/:" + returnHex)
        Return returnHex
    End Function
    Function fixNVItemNumberHalfway(ByVal itemLong As String) As String

        ''logger.addToLog(itemLong)

        Dim tempStr As String = ""
        For i As Integer = 0 To (itemLong.IndexOf("(") - 1)
            tempStr += itemLong(i)
        Next

        Return tempStr
    End Function


    'Function readNVItem(ByVal itemNumber As Integer, ByVal itemDebug As String) As Command

    '    ''build a command test evdo1
    '    Dim bob As New BuilderBob
    '    Dim result(2) As Byte



    '    ''fix the order the hex is displayed in
    '    Dim returnHex As String = fixNVItemNumber(itemNumber, 6)


    '    ''logger.addToLog("return hex revd/:" + returnHex)

    '    Dim prefix As Byte() = cdmaTerm.String_To_Bytes("26" + returnHex)
    '    ''no se? test
    '    '' cdmaTerm. dispatchQ.addCommandToQ(New Command((bob.buildATerminalCommand(cdmaTerm.empty_cmd_133, prefix, bob.buildDataArray("0" + "")))
    '    ''

    '    Dim fullCommand As New List(Of Byte)
    '    For Each b As Byte In prefix
    '        fullCommand.Add(b)
    '    Next

    '    While fullCommand.Count < 133
    '        fullCommand.Add(&H0)
    '    End While

    '    Dim calcCrc() As Byte
    '    calcCrc = cdmaTerm.gimmeCRC_AsByte_FromByte(fullCommand.ToArray)

    '    fullCommand.Add(calcCrc(0))

    '    fullCommand.Add(calcCrc(1))

    '    fullCommand.Add(&H7E)


    '    ''logger.addToLog("hex: " + returnHex + " prefix: " + cdmaTerm.biznytesToStrizings(prefix) + " whole command: " + cdmaTerm.biznytesToStrizings(fullCommand.ToArray))



    '    Dim returnTheCommand As New Command(fullCommand.ToArray, itemDebug)


    '    '' Dim returnTheCommand As New Command(bob.buildATerminalCommand(cdmaTerm.empty_cmd_133, prefix, bob.buildDataArray("0" + "")), "Return the Command")

    '    Return returnTheCommand
    'End Function
    Function readNVItemRange(ByVal startingItem As Integer, ByVal endItem As Integer) As Boolean

        Try

            Dim count As Integer = (endItem - startingItem)
            ''0 item err?
            'If endItem = startingItem Then
            '    ''count += 1
            'End If

            For i = startingItem To (count + startingItem)
                '' logger.addToLog("inside loop")
                ''Dim debugString As String = "NVread Item " + i.ToString
                Dim debugString As String = "readNVItemRange Qcdm.Cmd.DIAG_NV_READ_F " + i.ToString


                ''cdmaTerm.dispatchQ.addCommandToQ(readNVItem(i, debugString))

                cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_NV_READ_F, i, New Byte() {}, debugString))


            Next

            Return True
        Catch ex As Exception
            logger.addToLog("err:" + ex.ToString)
        End Try
        Return False

    End Function

    ''todo: this could be refactored out to a super/abstract class with subclasses for diff cdws versions if needed
    Function writeNVItemRange(ByVal fileName As String) As Integer
        ''this is what the file to write looks like

        '        [NV Items]

        ''if ver 2.7
        '[Complete items - 12]
        ''if ver 3.5
        '[Complete items - 12, Items size - 128]

        '0318 (0x013E)   -   OK
        '1F 31 32 33 34 35 36 37 38 39 30 40 65 78 61 6D 
        '70 6C 65 63 65 6C 6C 75 6C 61 72 31 2E 6E 65 74 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        '00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 

        ''open a reader(fileName), read into array?

        Dim fileTxt As New ArrayList '' = ArrayListReadNVItemFile("NVWrite.txt")

        fileTxt.AddRange(ReadNVItemFile(fileName))
        ''if file does not start with [NV Items] then fail
        If (fileTxt.Item(0) = ("[NV Items]")) Then

            '' logger.addToLog("NV Item File Found")
            For i As Integer = 3 To fileTxt.Count

                Dim nItemNumber As String = ""
                Dim nItemData As String = ""

                nItemNumber = fixNVItemNumberHalfway(fileTxt.Item(i))


                nItemData += fileTxt.Item(i + 1)
                nItemData += fileTxt.Item(i + 2)
                nItemData += fileTxt.Item(i + 3)
                nItemData += fileTxt.Item(i + 4)
                nItemData += fileTxt.Item(i + 5)
                nItemData += fileTxt.Item(i + 6)
                nItemData += fileTxt.Item(i + 7)
                nItemData += fileTxt.Item(i + 8)


                nItemData = nItemData.Replace(" ", "")
                ''logger.addToLog("NV Item: " + nItemNumber + " data: " + nItemData)

                WriteNVItem(nItemNumber, cdmaTerm.String_To_Bytes(nItemData))

                i = i + 13
            Next


        Else
            logger.addToLog("err: Bad nv file?")
        End If

        ''determine 3.5/2.7 - not sure if this will matter with our engine?
        '' looks like 3.5 supports variable lenth nv?


        '' loop to write nv item

        ''read numbers to first space to tmpNVItemNumber
        ''check if line ends with ok
        '' pad to 6 digits and do the number_flippy_stuff
        '' this is command ( 27 + number_flippy_stuff + data.(-spaces) + crc? + eof?  )


        ''return number of items written
        Return 0
    End Function


    ' ''TODO: this probably needs clean up and i think the builder bob part should/could be replaced
    'Public Function WriteNVItem(ByVal item As Integer, ByVal value As String) As Boolean
    '    Dim bob As New BuilderBob
    '    Dim enc As New ASCIIEncoding


    '    Dim prefix As Byte() = cdmaTerm.String_To_Bytes("27" + fixNVItemNumber(item, 4) + (value))
    '    Dim tempQ As New dispatchQmanager

    '    '' logger.addToLog(cdmaTerm.biznytesToStrizings(bob.buildATerminalCommand(cdmaTerm.empty_cmd_133, prefix, bob.buildDataArray("0" + "")))).ToString()

    '    ''Dim c As New Command(bob.buildATerminalCommand(cdmaTerm.empty_cmd_133, prefix, bob.buildDataArray("0" + "")), ("nv"))

    '    ''logger.addToLog("prefix" + cdmaTerm.biznytesToStrizings(prefix))
    '    ''TODO: Error
    '    ''logger.addToLog("wr nv" + cdmaTerm.biznytesToStrizings(bob.buildATerminalCommand(cdmaTerm.empty_cmd_133, prefix, bob.buildDataArray("0" + ""))))

    '    tempQ.addCommandToQ(New Command(bob.buildATerminalCommand(cdmaTerm.empty_cmd_133, prefix, bob.buildDataArray("0" + "")), ("nv#")))

    '    tempQ.executeCommandQ()
    '    Return True

    'End Function

    Public Function WriteNVItem(ByVal item As Integer, ByVal data As Byte()) As Boolean

        Dim logMsg = "DIAG_NV_WRITE item: " + item.ToString

        cdmaTerm.dispatchQ.addCommandToQ(New Command(Qcdm.Cmd.DIAG_NV_WRITE_F, data, logMsg))

        Return cdmaTerm.dispatchQ.executeCommandQ
    End Function
    Dim oFile As System.IO.File
    Dim oRead As System.IO.StreamReader
    Public Function ReadNVItemFile(ByVal fileName As String) As ArrayList

        oRead = IO.File.OpenText(fileName)

        Dim LineIn As New ArrayList
        While oRead.Peek <> -1
            LineIn.Add(oRead.ReadLine())

        End While

        oRead.Close()
        Return LineIn
    End Function








End Class
